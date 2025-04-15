namespace Finance.Application.Goals.Commands.CreateGoal;

public class CreateGoalCommandValidator : AbstractValidator<CreateGoalCommand>
{
    public CreateGoalCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(200).WithMessage("Name cannot be longer than 200 characters.")
            .NotEmpty().WithMessage("Name cannot be empty");
    }
}
