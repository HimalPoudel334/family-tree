
namespace FamilyTreeData.Repositories.Base;

public interface IBaseRepository<T> where T : class
{
    Task<IList<T>> GetAllAsync();
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> GetQueryable();
    Task<T?> GetByIdAsync(int id);
}

