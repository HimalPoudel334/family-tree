namespace FamilyTree.Helpers;

public class FileUploadHelper : IFileUploadHelper 
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadHelper(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string ReturnUniqueFileName(IFormFile file)
    {
        if (file == null) throw new Exception("Image not uploaded");
        
        return Guid.NewGuid().ToString() + "_" + file.FileName;

    }
    
    public async Task SaveFile(IFormFile file, string uniqueFileName, string uploadsDirectory = "persons")
    {
        var fileFullPath = GetFileFullPath(uniqueFileName, uploadsDirectory);
        
        await using var fileStream = new FileStream(fileFullPath, FileMode.Create);
        await file.CopyToAsync(fileStream);
    }

    public void RemoveFile(string fileName, string uploadsDirectory = "persons")
    {
        var fileFullPath = GetFileFullPath(fileName, uploadsDirectory);
        if (File.Exists(fileFullPath))
        { 
            File.Delete(fileFullPath);
        }
    }

    private string GetFileFullPath(string fileName, string uploadsDirectory = "persons")
    {
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", uploadsDirectory);
        return Path.Combine(uploadsFolder, fileName);
    }
}