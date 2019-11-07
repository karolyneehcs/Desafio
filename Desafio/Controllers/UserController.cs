using Desafio.Context;
using Desafio.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpGet]
        [Authorization]
        public IActionResult Get()
        {
            var user = HttpContext.Request.Headers["Authorization"];
            return Ok();
        }

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok();
        }
    }
}