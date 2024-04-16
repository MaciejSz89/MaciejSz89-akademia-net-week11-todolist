using ToDoList.Core.Dtos;

namespace ToDoList.WebApi.Core.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);
        void RegisterUser(RegisterUserDto registerUserDto);
    }
}
