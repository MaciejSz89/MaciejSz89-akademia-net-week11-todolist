using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Converters;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Models.Dtos;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Exceptions;

namespace ToDoList.WebApi.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _unitOfWork.User.Get(dto.Email);

            if(user == null)
                throw new BadRequestException("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role?.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, 
                                             _authenticationSettings.JwtIssuer, 
                                             claims, 
                                             expires: expires, 
                                             signingCredentials: cred);

            var tokenHadler = new JwtSecurityTokenHandler();
            return tokenHadler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            var user = registerUserDto.ToDao(_passwordHasher);
            _unitOfWork.User.Add(user);
            _unitOfWork.Complete();
        }
    }
}
