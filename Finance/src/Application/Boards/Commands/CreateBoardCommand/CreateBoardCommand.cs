using Finance.Application.Common.Interfaces;
using Finance.Domain.Entities;

namespace Finance.Application.Boards.Commands.CreateBoardCommand;

public record CreateBoardCommand(string Name) : IRequest<int>;
public class CreateBoardCommandHandler(IApplicationDbContext context, IUser user) : IRequestHandler<CreateBoardCommand, int>
{
    public async Task<int> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = new Board { Name = request.Name };
        
        if(user?.Id is null)
            throw new UnauthorizedAccessException();
        
        entity.UserIds ??= new List<string>();
        entity.UserIds.Add(user.Id!);
        
        entity.AdminIds ??= new List<string>();
        entity.AdminIds.Add(user.Id!);
        
        context.Boards.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
