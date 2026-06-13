namespace Domain.Ports.Services;

public interface IExcelExportService
{
    byte[] ExportToTable<T>(IEnumerable<T> data, string sheetName) where T : class;
}