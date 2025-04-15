using Finance.Domain.Entities;

namespace Finance.Application.Goals.Queries.GetGoalsWithPagination;

public class GoalBriefDto
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public DateOnly DeadLineDate { get; set; }
    public int? OwnerUserId { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Goal, GoalBriefDto>();
        }
    }
}
