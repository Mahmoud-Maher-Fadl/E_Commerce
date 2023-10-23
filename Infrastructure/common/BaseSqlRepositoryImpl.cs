using Domain.common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.common;

public class BaseSqlRepositoryImpl<T>:IBaseRepository<T> where T:BaseEntity
{
    protected readonly DbSet<T> Table;

    public BaseSqlRepositoryImpl(DbSet<T> table)
    {
        Table = table;
    }

    public async Task<Result<T>> Add(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await Table.AddAsync(entity, cancellationToken);
            return result.Entity.AsSuccessResult();
        }
        catch (Exception e)
        {
            return e.Message.AsFailureResult<T>();
        }
    }

    public Task<Result<T>> Update(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.UpdateDate=DateTime.Now;
            var result = Table.Update(entity);
            return Task.FromResult(result.Entity.AsSuccessResult());
        }
        catch (Exception e)
        {
            return Task.FromResult(e.Message.AsFailureResult<T>());
        }
    }

    public Task<Result<T>> Delete(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = Table.Remove(entity);
            return Task.FromResult<Result<T>>(result.Entity.AsSuccessResult());
        }
        catch (Exception e)
        {
            return Task.FromResult(e.Message.AsFailureResult<T>());
        }
    }

    public async Task<Result<T>> DeleteById(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await Table.FindAsync(id);
            if (entity is null)
                return $"{typeof(T).Name} not found".AsFailureResult<T>();
            var result = Table.Remove(entity);
            return result.Entity.AsSuccessResult();
        }
        catch (Exception e)
        {
            return e.Message.AsFailureResult<T>();
        }
    }
}