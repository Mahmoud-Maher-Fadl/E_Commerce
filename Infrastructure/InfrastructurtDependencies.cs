using Domain.common;
using Domain.Model.Category;
using Domain.Model.Item;
using Infrastructure.Category;
using Infrastructure.common;
using Infrastructure.Item;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>()
                          .AddTransient<IItemRepository, ItemRepository>()
                           .AddTransient(typeof(IBaseRepository<>),typeof(BaseSqlRepositoryImpl<>));
        return serviceCollection;
    }
}