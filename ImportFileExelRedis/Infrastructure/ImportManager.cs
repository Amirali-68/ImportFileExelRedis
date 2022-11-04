using Application.Publisher;
using ImportFileExcelRedis.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace ImportFileExcelRedis.Infrastructure
{
    public class ImportManager : IImportManager
    {
        private readonly IEventPublisher _eventPublisher;

        public ImportManager(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public static IList<PropertyByName<T>> GetPropertiesByExcelCells<T>(ExcelWorksheet worksheet)
        {
            var properties = new List<PropertyByName<T>>();
            var poz = 1;
            while (true)
            {
                try
                {
                    var cell = worksheet.Cells[1, poz];

                    if (string.IsNullOrEmpty(cell?.Value?.ToString()))
                        break;

                    poz += 1;
                    properties.Add(new PropertyByName<T>(cell.Value.ToString()));
                }
                catch
                {
                    break;
                }
            }

            return properties;
        }

        public async Task FromXlsxAsync(Stream stream)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var xlPackage = new ExcelPackage(stream);
            // get the first worksheet in the workbook
            var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new Exception("No worksheet found");

            var dataRow = new List<ExcelModel>();
            var properties = GetPropertiesByExcelCells<object>(worksheet);
            var manager = new PropertyManager<object>(properties);
            int endRow = 2;
            var columns = worksheet.Columns.Count();
            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                              .Select(property => worksheet.Cells[endRow, property.PropertyOrderPosition])
                              .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                if (allColumnsAreEmpty)
                    break;

                for (int i = 1; i <= columns; i++)
                {
                    var row = new ExcelModel
                    {
                        RowName = worksheet.Cells[1, i].Value?.ToString() ?? "0",
                        Value = worksheet.Cells[endRow, i].Value?.ToString() ?? "0",
                    };
                    dataRow.Add(row);
                }

                endRow++;
            }

            string serialized = JsonConvert.SerializeObject(dataRow);
            await _eventPublisher.PublishAsync(serialized);
        }
    }
}
