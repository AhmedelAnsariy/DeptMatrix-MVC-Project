using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVCRev.PL.Helper
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            string fileName = $"{Guid.NewGuid()}_{file.FileName}";

            string filePath = Path.Combine(folderpath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);


            return fileName;


        }
        

        public static void DeleteFiel(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName , fileName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }


        }

    }
}
