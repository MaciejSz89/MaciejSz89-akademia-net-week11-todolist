using FluentValidation;

namespace ToDoList.WebApi.Core.Models.Validators
{
    public class GetTasksParamsValidator : AbstractValidator<GetTasksParams>
    {
        public GetTasksParamsValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (value <= 0 || value % 5 != 0)
                {
                    context.AddFailure("PageSize", $"PageSize must be divisible by 5");
                }
            });

        }
    }
}
