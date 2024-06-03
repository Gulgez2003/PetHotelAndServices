namespace FinalProjectApp.Application.Extensions
{
    public static class FileExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            if (file == null) return false;
            return file.ContentType.Contains("image");
        }

        public static bool IsSizeOk(this IFormFile file, int mb)
        {
            if (file == null) return false;
            return file.Length / 1024 / 1024 < mb;
        }
        public static string CreateFile(this IFormFile formFile, string env, string path)
        {
            string fileName = $"{Guid.NewGuid()}{formFile.FileName}";
            string fullPath = Path.Combine(env, path, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}
