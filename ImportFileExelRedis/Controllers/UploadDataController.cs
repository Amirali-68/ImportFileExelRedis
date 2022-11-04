using ImportFileExcelRedis.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ImportFileExcelRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadDataController : ControllerBase
    {
        private readonly IImportManager _importManager;
        public UploadDataController(IImportManager importManager)
        {
            _importManager = importManager;
        }
        [HttpPost]
        public async Task<IActionResult> UploadData(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    await _importManager.FromXlsxAsync(file.OpenReadStream());
                    return Ok("با موفقیت ثبت شد");
                }
                return NotFound("لطفا یک فایل اکسل انتخاب کنید");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}