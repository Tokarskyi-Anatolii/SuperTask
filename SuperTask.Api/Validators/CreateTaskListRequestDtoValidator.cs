using FluentValidation;
using SuperTaskTracking.DTO_s;

namespace SuperTaskTracking.Validators;

public class CreateTaskListRequestDtoValidator : AbstractValidator<CreateTaskListRequestDto>
{
    public CreateTaskListRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            
            .MaximumLength(255)
            .WithMessage("Name must not exceed 255 characters");
    }
}