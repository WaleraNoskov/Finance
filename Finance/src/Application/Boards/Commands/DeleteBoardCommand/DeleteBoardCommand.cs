using Finance.Application.Common.Interfaces;

namespace Finance.Application.Boards.Commands.DeleteBoardCommand;

public record DeleteBoardCommand(int Id, string CurrentUserId) : IRequest;

public class DeleteBoardCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteBoardCommand>
{
    public async Task Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Boards
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        if(!entity.AdminIds!.Contains(request.CurrentUserId))
            throw new UnauthorizedAccessException();

        context.Boards.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
