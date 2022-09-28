using Business.Abstract;
using Entities.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailParameterController : ControllerBase
    {
        private readonly IMailParemeterService _mailParemeterService;

        public MailParameterController(IMailParemeterService mailParemeterService)
        {
            _mailParemeterService = mailParemeterService;
        }
        [HttpPost("update")]
        public IActionResult Update(MailParemeter mailParemeter)
        {
            var result = _mailParemeterService.Update(mailParemeter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
