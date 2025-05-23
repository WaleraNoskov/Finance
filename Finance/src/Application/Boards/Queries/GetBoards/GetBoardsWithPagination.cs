using Finance.Application.Common.Interfaces;
using Finance.Application.Common.Mappings;
using Finance.Application.Common.Models;
using Finance.Application.Common.Security;
using Finance.Domain.Entities;

namespace Finance.Application.Boards.Queries.GetBoards;

[Authorize]
public record GetBoardsWithPagination : IRequest<PaginatedList<BoardBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBoardsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user) 
    : IRequestHandler<GetBoardsWithPagination, PaginatedList<BoardBriefDto>>
{
    public async Task<PaginatedList<BoardBriefDto>> Handle(GetBoardsWithPagination request, CancellationToken cancellationToken)
    {
        if(user?.Id is null)
            throw new UnauthorizedAccessException();
        
        return await context.Boards
            .Include(b => b.UserIds)
            .Where(b => b.UserIds != null && b.UserIds.Contains(user.Id))
            .OrderBy(b => b.Name)
            .ProjectTo<BoardBriefDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}


