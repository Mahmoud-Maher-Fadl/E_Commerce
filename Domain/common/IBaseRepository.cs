namespace Domain.common;

public interface IBaseRepository<T> where T:BaseEntity
{ 
    Task<Result<T>> Add(T entity,CancellationToken cancellationToken =default);
    Task<Result<T>> Update(T entity,CancellationToken cancellationToken =default);
    Task<Result<T>> Delete(T entity,CancellationToken cancellationToken =default);
    Task<Result<T>> DeleteById(string id, CancellationToken cancellationToken = default);
}