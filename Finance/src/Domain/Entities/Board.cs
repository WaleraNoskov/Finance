namespace Finance.Domain.Entities;

public class Board : BaseAuditableEntity
{
    public string? Name { get; set; }
    public ICollection<Income>? Incomes { get; set; }
    public ICollection<Payment>? Payments { get; set; }
    public ICollection<Goal>? Goals { get; set; }
    
    public ICollection<string> UserIds { get; set; } = new List<string>();
}
