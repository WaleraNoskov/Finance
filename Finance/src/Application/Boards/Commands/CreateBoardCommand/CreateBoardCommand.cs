using Finance.Application.Common.Interfaces;
using Finance.Domain.Entities;

namespace Finance.Application.Boards.Commands.CreateBoardCommand;

public record CreateBoardCommand(string Name, string CurrentUserId) : IRequest<int>;
public class CreateBoardCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateBoardCommand, int>
{
    public async Task<int> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = new Board { Name = request.Name };
        
        entity.UserIds ??= new List<string>();
        entity.UserIds.Add(request.CurrentUserId);
        
        entity.AdminIds ??= new List<string>();
        entity.AdminIds.Add(request.CurrentUserId);
        
        context.Boards.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
