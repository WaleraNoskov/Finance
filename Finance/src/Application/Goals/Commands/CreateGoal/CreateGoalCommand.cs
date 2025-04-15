using Finance.Application.Common.Interfaces;
using Finance.Domain.Entities;

namespace Finance.Application.Goals.Commands.CreateGoal;

public class CreateGoalCommand : IRequest<int>
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public DateOnly DeadLineDate { get; set; }
    public int BoardId { get; set; }
    public int OwnerUserId { get; set; }
}

public class CreateGoalCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateGoalCommand, int>
{
    public async Task<int> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
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
