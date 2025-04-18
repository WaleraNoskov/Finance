using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.OpenAccessCommand;

public record OpenAccessCommand(int Id, string UserId) : IRequest;

public class OpenAccessCommandHandler(IApplicationDbContext context, IIdentityService identityService, IUser user) : IRequestHandler<OpenAccessCommand>
{
    public async Task Handle(OpenAccessCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards
            .FindAsync([request.Id], cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        if(await identityService.UserExists(request.UserId) == false)
            throw new NotFoundException(request.UserId, nameof(user));
        if(user?.Id is null || !entity.AdminIds!.Contains(user.Id))
            throw new UnauthorizedAccessException();
        
        entity.UserIds!.Add(request.UserId);
        await context.SaveChangesAsync(cancellationToken);
    }
}
