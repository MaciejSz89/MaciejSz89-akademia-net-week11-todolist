using Microsoft.AspNetCore.Identity;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Models.Dtos;

namespace ToDoList.WebApi.Core.Models.Converters
{
    public static class UserConverter
    {
        public static User ToDao(this RegisterUserDto registerUserDto, IPasswordHasher<User> passwordHasher)
        {
            var user = new User();
            var hashedPassword = passwordHasher.HashPassword(user, registerUserDto.Password);
            return new User
            {
                Email = registerUserDto.Email,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                PasswordHash = hashedPassword,
                RoleId = 1
            };
        }
    }
}
