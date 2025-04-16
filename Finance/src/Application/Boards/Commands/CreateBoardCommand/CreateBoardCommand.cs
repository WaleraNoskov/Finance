using Finance.Application.Common.Interfaces;
using Finance.Domain.Entities;

namespace Finance.Application.Boards.Commands.CreateBoardCommand;

public class CreateBoardCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateBoardCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateBoardCommand, int>
{
    public async Task<int> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var entity = new Board { Name = request.Name };
        
        context.Boards.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
