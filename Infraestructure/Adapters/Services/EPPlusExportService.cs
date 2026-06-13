using System.Reflection;
using Domain.Ports.Services;
using OfficeOpenXml;

namespace Infraestructure.Adapters.Services;

public class EpPlusExportService : IExcelExportService
{
    public byte[] ExportToTable<T>(IEnumerable<T> data, string sheetName) where T : class
    {
        // 1. En EPPlus usamos ExcelPackage en lugar de XLWorkbook
        using var package = new ExcelPackage();
        
        // Agregar la hoja de trabajo
        var worksheet = package.Workbook.Worksheets.Add(sheetName);

        // 2. Obtener las propiedades públicas de la entidad para las cabeceras
        PropertyInfo[] properties = typeof(T).GetProperties(
            BindingFlags.Public | BindingFlags.Instance
        );
        
        // Escribir cabeceras
        for (int col = 0; col < properties.Length; col++)
        {
            worksheet.Cells[1, col + 1].Value = properties[col].Name;
        }
        
        // Escribir los datos
        int currentRow = 2;
        foreach (var item in data)
        {
            for (int col = 0; col < properties.Length; col++)
            {
                var value = properties[col].GetValue(item, null);
                // EPPlus maneja tipos nativos (int, DateTime, string) directamente sin forzar .ToString()
                worksheet.Cells[currentRow, col + 1].Value = value; 
            }
            currentRow++;
        }
        
        // 3. Crear la Tabla y Autoajustar columnas si hay datos
        if (currentRow > 2)
        {
            // Definir el rango (FilaInicio, ColInicio, FilaFin, ColFin)
            var range = worksheet.Cells[1, 1, currentRow - 1, properties.Length];
            
            // Crear la tabla con el estilo por defecto (similar a ClosedXML)
            string tableName = $"Table_{sheetName.Replace(" ", "_")}";
            worksheet.Tables.Add(range, tableName);
            
            // En EPPlus, por defecto las tablas ya incluyen el diseño de filas alternas.
            // Si quieres un estilo específico, puedes usar:
            // table.TableStyle = TableStyles.Medium2;

            // Autoajustar el ancho de las columnas
            worksheet.Cells[1, 1, currentRow - 1, properties.Length].AutoFitColumns();
        }
        
        // 4. Guardar en el MemoryStream
        // EPPlus permite obtener los bytes directamente o guardar al stream
        return package.GetAsByteArray();
    }
}