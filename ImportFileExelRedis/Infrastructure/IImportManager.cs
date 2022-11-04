namespace ImportFileExelRedis.Infrastructure
{
    public interface IImportManager
    {
        Task FromXlsxAsync(Stream stream);
    }
}
