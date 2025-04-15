using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities;

public class Goal : BaseAuditableEntity
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }
    public DateOnly DeadLineDate { get; set; }
    
    public int BoardId { get; set; }
    [ForeignKey("BoardId")] public Board? Board { get; set; }
    
    public int? OwnerUserId { get; set; }
}
