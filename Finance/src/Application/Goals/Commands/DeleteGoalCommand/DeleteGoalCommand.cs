using Finance.Application.Common.Interfaces;

namespace Finance.Application.Goals.Commands.DeleteGoalCommand;

public record DeleteGoalCommand(int Id) : IRequest;

public class DeleteGoalCommandHandler(IApplicationDbContext context, IUser user) : IRequestHandler<DeleteGoalCommand>
{
    public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Goals
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        if (user?.Id is null || !entity.Board!.UserIds!.Contains(user.Id))
            throw new UnauthorizedAccessException();

        context.Goals.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
