using System.ComponentModel.DataAnnotations;

namespace FamilyTreeLib.Entities;

public class Person
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public DateTime Dob { get; set; }
    public string? WifeName { get; set; }
    public int? FatherId { get; set; }
    public Person? Father { get; set; }
    public int? MotherId { get; set; }
    public Person? Mother { get; set; }
    public virtual ICollection<Person> Childrens { get; set; }

}