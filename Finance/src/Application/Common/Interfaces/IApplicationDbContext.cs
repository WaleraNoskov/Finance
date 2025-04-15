using Finance.Domain.Entities;

namespace Finance.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    
    DbSet<Board> Boards { get; }
    
    DbSet<Income> Incomes { get; }
    
    DbSet<Payment> Payments { get; }
    
    DbSet<Goal> Goals { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
