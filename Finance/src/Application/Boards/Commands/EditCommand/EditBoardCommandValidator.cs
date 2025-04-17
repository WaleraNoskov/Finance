namespace Finance.Application.Boards.Commands.EditCommand;

public class EditBoardCommandValidator : AbstractValidator<EditBoardCommand>
{
    public EditBoardCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .NotEmpty();
        
        RuleFor(x => x.CurrentUserId)
            .NotEmpty();
    }
}
