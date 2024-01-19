using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Dtos;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Persisntence;
using ToDoList.WebApi.Exceptions;

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
