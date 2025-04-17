namespace Finance.Application.Goals.Commands.EditGoalCommand;

public class EditGoalCommandValidator : AbstractValidator<EditGoalCommand>
{
    public EditGoalCommandValidator()
    {
        RuleFor(x => x.CurrentUserId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .NotEmpty();
    }
}
