using System.Reflection;
using ClosedXML.Excel;
using Domain.Ports.Services;

namespace Infraestructure.Adapters.Services;

public class ExcelExportService : IExcelExportService
{
    public byte[] ExportToTable<T>(IEnumerable<T> data, string sheetName) where T : class
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(sheetName);

        // 1. Obtener las propiedades públicas de la entidad para las cabeceras
        PropertyInfo[] properties = typeof(T).GetProperties(
            BindingFlags.Public | BindingFlags.Instance
        );
        
        for (int col = 0; col < properties.Length; col++)
        {
            worksheet.Cell(1, col + 1).Value = properties[col].Name;
        }
        
        int currentRow = 2;
        foreach (var item in data)
        {
            for (int col = 0; col < properties.Length; col++)
            {
                var value = properties[col].GetValue(item, null);
                worksheet.Cell(currentRow, col + 1).Value = value?.ToString() ?? string.Empty;
            }
            currentRow++;
        }
        
        if (currentRow > 2)
        {
            var range = worksheet.Range(
                1,
                1,
                currentRow - 1,
                properties.Length);
            
            var table = range.CreateTable();
            
            worksheet.Columns(1, properties.Length).AdjustToContents();
        }
        
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        
        return stream.ToArray();
    }
}