using Business.Abstract;
using Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconiliationController : ControllerBase
    {
        private readonly IAccountReconiliationService _accountReconiliationService;

        public AccountReconiliationController(IAccountReconiliationService accountReconiliationService)
        {
            _accountReconiliationService = accountReconiliationService;
        }

        [HttpPost("addFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int companyId)
        {
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".xlsx";
                var filePath = $"{Directory.GetCurrentDirectory()}/Content/{fileName}";
                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }
                var result = _accountReconiliationService.AddToExcel(filePath, companyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya seçimi yapmadınız");
        }

        [HttpGet("getlist")]
        public IActionResult GetList(int companyId)
        {
            var result = _accountReconiliationService.GetList(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _accountReconiliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(AccountReconiliation accountReconiliation)
        {
            var result = _accountReconiliationService.Add(accountReconiliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconiliation accountReconiliation)
        {
            var result = _accountReconiliationService.Update(accountReconiliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(AccountReconiliation accountReconiliation)
        {
            var result = _accountReconiliationService.Delete(accountReconiliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
