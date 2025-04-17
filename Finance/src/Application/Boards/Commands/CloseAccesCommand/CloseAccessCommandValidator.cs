namespace Finance.Application.Boards.Commands.CloseAccesCommand;

public class CloseAccessCommandValidator : AbstractValidator<CloseAccessCommand>
{
    public CloseAccessCommandValidator()
    {
        RuleFor(x => x.CurrentUserId)
            .NotEmpty();
    }
}
