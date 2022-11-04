namespace ImportFileExcelRedis.Infrastructure
{
    public interface IImportManager
    {
        Task FromXlsxAsync(Stream stream);
    }
}
