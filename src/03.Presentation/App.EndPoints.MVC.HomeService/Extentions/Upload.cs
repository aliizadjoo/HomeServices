namespace App.EndPoints.MVC.HomeService.Extentions
{
    public static class Upload
    {

        public static string? UploadFile(this IFormFile? file, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

           
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
                return null; 

            var directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                folderName
            );

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = Guid.NewGuid() + fileExtension;
            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }


        public static List<string> UploadFiles(this List<IFormFile>? files, string folderName)
        {
            var paths = new List<string>();

            if (files == null || !files.Any())
                return paths;

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = file.UploadFile(folderName);

                    if (!string.IsNullOrEmpty(path))
                        paths.Add(path);
                }
            }

            return paths;
        }

    }
}
