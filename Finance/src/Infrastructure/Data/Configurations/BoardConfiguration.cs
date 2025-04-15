using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Data.Configurations;

public class BoardConfiguration: IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(b => b.Incomes);
        builder.HasMany(b => b.Payments);
        builder.HasMany(b => b.Goals);
    }
}
