using Business.Abstract;
using Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaBsReconciliationsDetailController : ControllerBase
    {
        private readonly IBaBsReconciliationsDetailService _baBsReconciliationsDetailService;

        public BaBsReconciliationsDetailController(IBaBsReconciliationsDetailService baBsReconciliationsDetailService)
        {
            _baBsReconciliationsDetailService = baBsReconciliationsDetailService;
        }

        [HttpPost("add")]
        public IActionResult Add(BaBsReconciliationsDetail baBsReconciliationsDetail)
        {
            var result = _baBsReconciliationsDetailService.Add(baBsReconciliationsDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BaBsReconciliationsDetail baBsReconciliationsDetail)
        {
            var result = _baBsReconciliationsDetailService.Delete(baBsReconciliationsDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("addFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int babsReconcilationId)
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
                var result = _baBsReconciliationsDetailService.AddToExcel(filePath, babsReconcilationId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya seçimi yapmadınız");
        }

        [HttpGet("getlist")]
        public IActionResult GetList(int babsReconcilationId)
        {
            var result = _baBsReconciliationsDetailService.GetList(babsReconcilationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _baBsReconciliationsDetailService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
       
    }
}
