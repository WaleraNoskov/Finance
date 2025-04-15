using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities;

public class Income : BaseAuditableEntity
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Periodicity Periodicity { get; set; }

    public int BoardId { get; set; }
    [ForeignKey("BoardId")] public Board? Board { get; set; }
    
    public int? SourceUserId { get; set; }
}
