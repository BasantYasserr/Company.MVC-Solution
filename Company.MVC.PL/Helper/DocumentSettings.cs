namespace Company.MVC.PL.Helper
{
    public static class DocumentSettings
    {
        //1.Upload
        public static string Upload(IFormFile file, string folderName)
        {
            //1. Get Location Of Folder
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}");


            //2. Get File Name and Must Be Unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path: FolderPath + FileName --> Combining Folder Path and File Name 
            string filePath = Path.Combine(folderPath, fileName);

            //4. File Stream
            using var FileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(FileStream);

            return fileName;

        }

        //2.Delete
        public static void Delete(string fileName, string folderName)
        {
            //1. Get The Location
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}", fileName);

            //2. Delete File
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
