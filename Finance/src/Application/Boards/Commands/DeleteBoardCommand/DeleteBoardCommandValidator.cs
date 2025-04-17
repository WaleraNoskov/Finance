namespace Finance.Application.Boards.Commands.DeleteBoardCommand;

public class DeleteBoardCommandValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardCommandValidator()
    {
        RuleFor(x => x.CurrentUserId)
            .NotEmpty();
    }
}
