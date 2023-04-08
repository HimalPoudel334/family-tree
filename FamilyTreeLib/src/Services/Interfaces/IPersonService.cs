using FamilyTreeLib.Dtos;

namespace FamilyTreeLib.Services.Interfaces;

public interface IPersonService {
  Task Add(PersonCreateDto dto);
  Task Update();
}
