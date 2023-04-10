using System.Drawing;
using System.Text.RegularExpressions;

namespace FamilyTree.Extensions;

//This class is directly copied from stackoverflow https://stackoverflow.com/questions/11063900/determine-if-uploaded-file-is-image-any-format-on-mvc

public static class FormFileExtensions
{
    private const int ImageMinimumBytes = 512;

    public static bool IsImage(this IFormFile postedFile)
    {
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        if (postedFile.ContentType.ToLower() != "image/jpg" &&
                    postedFile.ContentType.ToLower() != "image/jpeg" &&
                    postedFile.ContentType.ToLower() != "image/pjpeg" &&
                    postedFile.ContentType.ToLower() != "image/gif" &&
                    postedFile.ContentType.ToLower() != "image/x-png" &&
                    postedFile.ContentType.ToLower() != "image/png")
        {
            return false;
        }

        //-------------------------------------------
        //  Check the image extension
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
        {
            return false;
        }

        //-------------------------------------------
        //  Attempt to read the file and check the first bytes
        //-------------------------------------------
        try
        {
            if (!postedFile.OpenReadStream().CanRead)
            {
                return false;
            }
            //------------------------------------------
            //check whether the image size exceeding the limit or not
            //------------------------------------------ 
            if (postedFile.Length < ImageMinimumBytes)
            {
                return false;
            }

            byte[] buffer = new byte[ImageMinimumBytes];
            postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
            string content = System.Text.Encoding.UTF8.GetString(buffer);
            if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
            {
                return false;
            }
            
            //signature check of image
            // JPEG signature: FF D8 FF E0 or FF D8 FF E1
            // PNG signature: 89 50 4E 47 0D 0A 1A 0A
            // GIF signature: 47 49 46 38 or 47 49 46 39
            if (!((buffer[0] == 0xFF &&
                   buffer[1] == 0xD8 && 
                   (buffer[2] == 0xFF && buffer[3] == 0xE0 || buffer[2] == 0xFF && buffer[3] == 0xE1)) ||
                  (buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47 && buffer[4] == 0x0D && buffer[5] == 0x0A &&
                   buffer[6] == 0x1A && buffer[7] == 0x0A) ||
                  (buffer[0] == 0x47 && buffer[1] == 0x49 && buffer[2] == 0x46
                   && (buffer[3] == 0x38 || buffer[3] == 0x39))))
            {
                // file signature does not match any of the supported image formats
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
           
        return true;
    }

}

