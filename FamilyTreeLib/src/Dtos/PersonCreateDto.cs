namespace FamilyTreeLib.Dtos;

public class PersonCreateDto
{
    public string FirstName { get; set; }
    public string? WifeName { get; set; }
    public int FatherId { get; set; }
    public string Mother { get; set; }
    public DateTime Dob { get; set; }
    public string Image { get; set; }
    public string? WifeImage { get; set; }
}