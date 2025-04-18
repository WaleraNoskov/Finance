using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.EditCommand;

public record EditBoardCommand(int Id, string Name) : IRequest;

public class EditBoardCommandHandler(IApplicationDbContext context, IUser user) : IRequestHandler<EditBoardCommand>
{
    public async Task Handle(EditBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        if (user?.Id is null || !entity.AdminIds!.Contains(user.Id))
            throw new UnauthorizedAccessException();
        
        entity.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);
    }
}
