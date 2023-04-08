using FamilyTreeData.AppDbContext;
using FamilyTreeData.Repositories.Base;
using FamilyTreeLib.Entities;
using FamilyTreeLib.Repositories;

namespace FamilyTreeData.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository {
    
    public PersonRepository(FamilyTreeDbContext context): base(context)
    {
        
    }
}