using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Services
{
    public interface IFileService
    {
        string SaveFile(IFormFile file);
        bool DeleteFile(string filePath);
        string EditFile(IFormFile newFile, string oldFile);
    }
}
