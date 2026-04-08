namespace SarjuPos.API.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file, string subFolder);
        void DeleteImage(string? imagePath);
    }

    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string subFolder)
        {
            if (file == null || file.Length == 0) return string.Empty;

            var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", subFolder);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path
            return Path.Combine("uploads", subFolder, fileName).Replace("\\", "/");
        }

        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;

            var filePath = Path.Combine(_environment.WebRootPath, imagePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
