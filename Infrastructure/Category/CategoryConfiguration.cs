using Infrastructure.common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Category;

public class CategoryConfiguration:BaseConfiguration<Domain.Model.Category.Category>
{
    protected override void Configure(EntityTypeBuilder<Domain.Model.Category.Category> builder, string tableName)
    {
        
    }
}