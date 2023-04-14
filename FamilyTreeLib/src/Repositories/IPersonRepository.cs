using FamilyTreeLib.Entities;

namespace FamilyTreeLib.Repositories;

public interface IPersonRepository {
  Task InsertAsync(Person person);
  Task UpdateAsync(Person person);
  Task<Person?> GetByIdAsync(int id);
  Task<IList<Person>> GetAllAsync();
  IQueryable<Person> GetQueryable();

}