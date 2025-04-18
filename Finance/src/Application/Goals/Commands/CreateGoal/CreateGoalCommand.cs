using Finance.Application.Common.Interfaces;
using Finance.Domain.Entities;
using ValidationException = Finance.Application.Common.Exceptions.ValidationException;

namespace Finance.Application.Goals.Commands.CreateGoal;

public record CreateGoalCommand(string Name, decimal Amount, DateOnly DeadLineDate, int BoardId, string? OwnerUserId) : IRequest<int>;

public class CreateGoalCommandHandler(IApplicationDbContext context, IIdentityService identityService, IUser user) : IRequestHandler<CreateGoalCommand, int>
{
    public async Task<int> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var board = await context.Boards.FindAsync([request.BoardId], cancellationToken);
        Guard.Against.NotFound(request.BoardId, board, "Board not found");
        
        if (user?.Id is null || user is not null && !board.UserIds!.Contains(user.Id!))
            throw new UnauthorizedAccessException();
        
        if(request.OwnerUserId != null && !board.UserIds!.Contains(request.OwnerUserId))
            throw new UnauthorizedAccessException("User to add is non allowed to board.");
        
        if(request.OwnerUserId != null && await identityService.UserExists(request.OwnerUserId) != true)
            throw new NotFoundException(request.OwnerUserId, "User not found");
        
        var entity = new Goal
        {
            Name = request.Name,
            Amount = request.Amount,
            DeadLineDate = request.DeadLineDate,
            BoardId = request.BoardId,
            OwnerUserId = request.OwnerUserId
        };
        
        context.Goals.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
