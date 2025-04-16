namespace Finance.Application.Boards.Commands.CreateBoardCommand;

public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .NotEmpty();
    }
}
