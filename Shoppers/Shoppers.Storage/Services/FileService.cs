using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Services
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var rootPath = _webHostEnvironment.WebRootPath;
                 var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            return false;
        }

        public string EditFile(IFormFile newFile, string oldFile)
        {
            DeleteFile(oldFile);
            var filePath = SaveFile(newFile);
            return filePath;
        }

        public string SaveFile(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uniqueFileName =
                        string.Concat(
                            Path.GetFileNameWithoutExtension(fileName),
                            "_", Guid.NewGuid().ToString().AsSpan(0, 4),
                            Path.GetExtension(fileName));

            string fileUrl = "/uploads/" + uniqueFileName;
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);
            var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            fileStream.Close();
            return uniqueFileName;
        }
        
    }
}
