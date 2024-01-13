using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lumia.Utilities.Extetions
{
    public static class FileValidator
    {
        public static bool IsValid(this IFormFile file, string format = "image/")
        {
            if (file.ContentType.Contains(format)) return true;
            return false;
        }
        public static bool LimitSize(this IFormFile file, int limitSize = 10)
        {
            if (file.Length <= limitSize * 1024 *1024) return true;
            return false;
        }
        public static string GetGuidName(string fullFileName)
        {
            int score = fullFileName.LastIndexOf("_");
            if(score > 0) 
            {
                string guidName = fullFileName.Substring(0,score);
                return guidName;
            }
            return fullFileName;
        }
        public static string GetFileFormat(string fullFileName)
        {
            int score = fullFileName.LastIndexOf(".");
            if (score > 0)
            {
                string format = fullFileName.Substring(score);
                return format;
            }
            return fullFileName;
        }
        public static async Task<string> CreateFileAsync(this IFormFile file, string root, params string[] folders)
        {
            string originalName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string guidName = GetGuidName(originalName);
            string format = GetFileFormat(originalName);
            string fullFileName = guidName + format;

            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, fullFileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fullFileName;
        }

        public static async void DeleteFileAsync(this string fullFileName, string root, params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, fullFileName);

            if (File.Exists(path)) File.Delete(fullFileName);
        }

    }
}
