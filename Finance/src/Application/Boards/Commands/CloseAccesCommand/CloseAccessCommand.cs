using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.CloseAccesCommand;

public record CloseAccessCommand(int Id, string UserId, string CurrentUserId) : IRequest;

public class CloseAccessCommandHandler(IApplicationDbContext context) : IRequestHandler<CloseAccessCommand>
{
    public async Task Handle(CloseAccessCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards
            .FindAsync([request.Id], cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        if(!entity.AdminIds!.Contains(request.CurrentUserId))
            throw new UnauthorizedAccessException();
        if(!entity.UserIds!.Contains(request.UserId))
            throw new ArgumentException("User cannot access this board.");
        
        entity.UserIds!.Remove(request.UserId);
        await context.SaveChangesAsync(cancellationToken);
    }
}
