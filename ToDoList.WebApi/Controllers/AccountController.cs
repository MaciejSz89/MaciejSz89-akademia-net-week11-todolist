using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Dtos;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Models.Response;
using ToDoList.WebApi.Persisntence;
using ToDoList.WebApi.Persistence.Exceptions;

namespace ToDoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountController(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Register")]
        public Response RegisterUser([FromBody] RegisterUserDto dto)
        {
            
            var response = new Response();
            try
            {
                _accountService.RegisterUser(dto);
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source,
                                              exception.Message));
            }
            return response;
        }

        [HttpPost("Login")]
        public Response Login([FromBody]LoginDto dto)
        {
            var response = new DataResponse<string>();
            try
            {
                var token = _accountService.GenerateJwt(dto);
                response.Data = token;
                
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source,
                                             exception.Message));

            }

            return response;

        }
    }
}
