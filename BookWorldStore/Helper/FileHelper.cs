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

        public async Task<string[]> SaveFileAsync(IFormFile file, string folder)
        {
            string[] result = new string[2];
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
                    string pathForWeb = $"~/{folder}/{fileName}";
                    result[0] = fileName;
                    result[1] = pathForWeb;
                    return result;
                }
                else
                {
                    result[0] = "Can't save because file is null";
                    return result;
                }
            }
            catch (IOException io)
            {
                result[0] = io.Message;
                return result;
            }
        }

        public bool DeleteFileAsync(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
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
