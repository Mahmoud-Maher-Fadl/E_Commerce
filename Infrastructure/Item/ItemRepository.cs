using Domain.Model.Item;
using Infrastructure.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Item;

public class ItemRepository:BaseSqlRepositoryImpl<Domain.Model.Item.Item>,IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context.Items)
    {
    }
}