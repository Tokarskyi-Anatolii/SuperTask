using FluentValidation;
using SuperTaskTracking.DTO_s;

namespace SuperTaskTracking.Validators;

public class UpdateTaskListRequestDtoValidator : AbstractValidator<CreateTaskListRequestDto>
{
    public UpdateTaskListRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            
            .MaximumLength(255)
            .WithMessage("Name must not exceed 255 characters");
    }
}