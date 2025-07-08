using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(PolicyName = "AllowOnlyLocalhost")]
    [Authorize(AuthenticationSchemes = "LoginForMicrosoftUsers")]
    public class MicrosoftController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetMicrosoft()
        {
            return Ok("this is microsoft");
        }
    }
}
