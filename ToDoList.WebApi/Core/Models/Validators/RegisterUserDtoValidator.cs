using FluentValidation;
using ToDoList.WebApi.Core.Models.Dtos;

namespace ToDoList.WebApi.Core.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = !(unitOfWork.User.Get(value) is null);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email address already exists in database");
                    }
                });
        }
    }
}
