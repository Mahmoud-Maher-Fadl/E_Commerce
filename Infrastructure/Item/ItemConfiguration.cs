using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Item;

public class ItemConfiguration:BaseConfiguration<Domain.Model.Item.Item>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Item.Item> builder, string tableName)
    {
        builder.HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}