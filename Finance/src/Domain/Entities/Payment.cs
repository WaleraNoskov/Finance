using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Periodicity Periodicity { get; set; }
    
    public int BoardId { get; set; }
    [ForeignKey("BoardId")] public Board? Board { get; set; }
    
    public int IncomeId { get; set; }
    [ForeignKey("IncomeId")] public Income? Income { get; set; }
    
    public int? UserId { get; set; }
}
