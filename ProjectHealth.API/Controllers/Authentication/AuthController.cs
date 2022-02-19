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
        public async Task<ActionResult> Register(Registration register)
        {
            var Register = await _authLogic.Register(register);
            if (Register.IsSuccessful != true)
            {
                return BadRequest(Register.Message);
            }

            return Ok(Register.Message);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(Login login)
        {
            var Login = await _authLogic.Login(login);
            if(Login.IsSuccessful != true)
            {
                return BadRequest(Login.Message);
            }

            return Ok(Login.Message);
        }
    }
}
