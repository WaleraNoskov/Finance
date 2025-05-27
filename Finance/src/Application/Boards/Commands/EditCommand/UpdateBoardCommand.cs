using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.EditCommand;

public record UpdateBoardCommand(int Id, string Name) : IRequest;

public class EditBoardCommandHandler(IApplicationDbContext context, IUser user) : IRequestHandler<UpdateBoardCommand>
{
    public async Task Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards.FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        if (user?.Id is null || !entity.AdminIds!.Contains(user.Id))
            throw new UnauthorizedAccessException();
        
        entity.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);
    }
}
