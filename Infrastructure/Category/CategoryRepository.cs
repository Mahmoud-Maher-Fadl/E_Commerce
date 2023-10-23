using Domain.Model.Category;
using Infrastructure.common;

namespace Infrastructure.Category;

public class CategoryRepository:BaseSqlRepositoryImpl<Domain.Model.Category.Category>,ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context.Categories)
    {
    }
}