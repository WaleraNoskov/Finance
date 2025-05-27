namespace Finance.Application.Boards.Commands.EditCommand;

public class EditBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
{
    public EditBoardCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .NotEmpty();
    }
}
