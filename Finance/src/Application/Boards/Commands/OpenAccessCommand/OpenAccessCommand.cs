using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.OpenAccessCommand;

public record OpenAccessCommand(int Id, string UserId, string CurrentUserId) : IRequest;

public class OpenAccessCommandHandler(IApplicationDbContext context) : IRequestHandler<OpenAccessCommand>
{
    public async Task Handle(OpenAccessCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards
            .FindAsync([request.Id], cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        if(!entity.AdminIds!.Contains(request.CurrentUserId))
            throw new UnauthorizedAccessException();
        
        entity.UserIds!.Add(request.UserId);
        await context.SaveChangesAsync(cancellationToken);
    }
}
