using Finance.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Finance.Domain.Entities;

namespace Finance.Application.Boards.Queries.GetBoards;

public class BoardBriefDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Board, BoardBriefDto>();
        }
    }
}
