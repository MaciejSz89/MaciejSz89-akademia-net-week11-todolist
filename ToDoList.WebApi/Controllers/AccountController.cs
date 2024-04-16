using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Core.Services;
using ToDoList.Core.Dtos;

namespace ToDoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {


            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {

            var token = _accountService.GenerateJwt(dto);

            return Ok(token);

        }
    }
}
