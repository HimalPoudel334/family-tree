namespace FamilyTree.Helpers;

public interface IFileUploadHelper
{
    Task SaveFile(IFormFile file, string uniqueFileName, string uploadsDirectory = "persons");
    string ReturnUniqueFileName(IFormFile file);
    void RemoveFile(string fileName, string uploadsDirectory = "persons");
}