using System.Transactions;
using FamilyTreeLib.Dtos;
using FamilyTreeLib.Entities;
using FamilyTreeLib.Repositories;
using FamilyTreeLib.Services.Interfaces;
using FamilyTreeLib.Exceptions;

namespace FamilyTreeLib.Services;

public class PersonService : IPersonService
{

    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository repository)
    {
        _personRepository = repository;
    }

    public async Task Add(PersonCreateDto dto)
    {
        using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var father = await _personRepository.GetByIdAsync(dto.FatherId) ?? throw new FatherDoesNotExistException("Person must have a father");
        if (string.IsNullOrEmpty(father.WifeName)) throw new MotherNotFoundException("Children cannot exist without a mother");

        var person = new Person(dto.FirstName, father, dto.Dob, dto.Image, dto.WifeName, dto.WifeImage);

        await _personRepository.InsertAsync(person).ConfigureAwait(false);

        tx.Complete();
    }

    public Task Update()
    {
        throw new NotImplementedException();
    }
}
