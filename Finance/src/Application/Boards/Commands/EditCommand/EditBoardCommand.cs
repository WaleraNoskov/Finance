using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.EditCommand;

public record EditBoardCommand(int Id, string CurrentUserId, string Name) : IRequest;

public class EditBoardCommandHandler(IApplicationDbContext context) : IRequestHandler<EditBoardCommand>
{
    public async Task Handle(EditBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        if (!entity.AdminIds!.Contains(request.CurrentUserId))
            throw new UnauthorizedAccessException();
        
        entity.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);
    }
}
