using Finance.Application.Common.Interfaces;

namespace Finance.Application.Goals.Commands.EditGoalCommand;

public record EditGoalCommand(int Id, string CurrentUserId, string Name, decimal Amount, string? OwnerUserId) : IRequest<int>;

public class EditGoalCommandHandler(IApplicationDbContext context, IIdentityService identityService) : IRequestHandler<EditGoalCommand, int>
{
    public async Task<int> Handle(EditGoalCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Goals
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity, "Goal not found");

        if (!entity.Board!.UserIds!.Contains(request.CurrentUserId))
            throw new UnauthorizedAccessException();
        
        if(request.OwnerUserId != null && await identityService.UserExists(request.OwnerUserId) == false)
            throw new NotFoundException(request.OwnerUserId, "User not found");
        
        entity.Name = request.Name;
        entity.Amount = request.Amount;
        entity.OwnerUserId = request.OwnerUserId;
        
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

