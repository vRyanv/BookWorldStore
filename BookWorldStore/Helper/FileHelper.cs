using BookWorldStore.Models;
using BookWorldStore.Utils;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BookWorldStore.Helper
{
    public class FileHelper
    {
        private static FileHelper _instance;
        public static FileHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FileHelper();
                }
                return _instance;
            }
        }
        private FileHelper() { }

        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        { 
            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string filePath = Path.Combine(currentDirectory, "wwwroot", folder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return fileName;
                }
                else
                {
                    return "null";
                }
            }
            catch (Exception)
            {
                return "null";
            }
        }

        public bool DeleteFileAsync(string folder, string fileName)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(currentDirectory, "wwwroot", folder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}

