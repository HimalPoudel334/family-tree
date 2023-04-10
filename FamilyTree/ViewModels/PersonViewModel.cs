namespace FamilyTree.ViewModels;

public class PersonCreateViewModel
{
    public string FirstName { get; set; }
    public int FatherId { get; set; }
    public string MotherName { get; set; }
    public string? WifeName { get; set; }
    public bool HasSecondWife { get; set; } = false;
    public DateTime Dob { get; set; }
    public IFormFile Image { get; set; }
    public IFormFile? WifeImage { get; set; }
    public ICollection<ParentSelectViewModel> FatherList { get; set; } = new List<ParentSelectViewModel>();

}

public class ParentSelectViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
}
