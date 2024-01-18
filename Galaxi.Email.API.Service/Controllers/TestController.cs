using Galaxi.Email.API.Service.Models;
using Galaxi.Email.API.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Galaxi.Email.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public TestController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public IActionResult SendEmail(EmailDTO request)
        {
            //_emailService.SendEmail();
            return Ok();
        }
    }
}
