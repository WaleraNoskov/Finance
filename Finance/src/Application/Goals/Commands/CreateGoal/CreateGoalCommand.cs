using Finance.Application.Common.Interfaces;
using Finance.Domain.Entities;

namespace Finance.Application.Goals.Commands.CreateGoal;

public record CreateGoalCommand(string CurrentUser, string Name, decimal Amount, DateOnly DeadLineDate, int BoardId, string? OwnerUserId) : IRequest<int>;

public class CreateGoalCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateGoalCommand, int>
{
    public async Task<int> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var board = await context.Boards.FindAsync([request.BoardId], cancellationToken);
        Guard.Against.NotFound(request.BoardId, board, "Board not found");
        
        if (!board.UserIds!.Contains(request.CurrentUser))
            throw new UnauthorizedAccessException();
        
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
