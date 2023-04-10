using FamilyTreeData.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTreeData.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly FamilyTreeDbContext _context;
    private readonly DbSet<T> _dbSet;
    protected BaseRepository(FamilyTreeDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet;
    }

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}

