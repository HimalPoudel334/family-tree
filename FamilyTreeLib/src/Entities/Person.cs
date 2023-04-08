using System.ComponentModel.DataAnnotations;
using FamilyTreeLib.Exceptions;

namespace FamilyTreeLib.Entities;

public class Person
{
    public Person(string firstName, Person father, DateTime dob, string image, string? wifeName, string? wifeImage)
    {
        FirstName = firstName;
        Father = father;
        FatherId = father.Id;
        MotherName = father.WifeName ?? throw new MotherNotFoundException("Person must have a mother");
        Dob = dob;
        Image = image;
        WifeName = wifeName;
        WifeImage = wifeImage;
    }

    protected Person()
    {
        
    }
    
    [Key]
    public int Id { get; protected set; }

    public string FirstName { get; protected set; }
    public DateTime Dob { get; protected set; }
    public string? WifeName { get; protected set; }
    public int FatherId { get; protected set; }
    public virtual Person Father { get; protected set; }
    public string MotherName { get; protected set; }
    public string Image { get; protected set; }
    public string? WifeImage { get; protected set; }
    public virtual ICollection<Person> Children { get; protected set; }

}