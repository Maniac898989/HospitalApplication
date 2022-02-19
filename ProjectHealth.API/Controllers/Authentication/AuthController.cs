using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHealth.Logic.Auth;
using ProjectHealth.Models.WebModel;

namespace ProjectHealth.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthLogic _authLogic;
        public AuthController(IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(Login Login)
        {
            var login = await _authLogic.Register(Login);
            return Ok(login);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(Login Login)
        {
            var login = await _authLogic.Login(Login);
            return Ok(login);
        }
    }
}
