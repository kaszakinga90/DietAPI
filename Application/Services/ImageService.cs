using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    /// <summary>
    /// Serwis umożliwiający przechowywanie obrazów w Cloudinary
    /// </summary>
    public class ImageService
    {
        private readonly Cloudinary _cloudinary;

        // Inicjalizacja serwisu z konfiguracją Cloudinary
        public ImageService(IConfiguration config)
        {
            var acc = new Account
                (
                    config["Cloudinary:CloudName"],
                    config["Cloudinary:ApiKey"],
                    config["Cloudinary:ApiSecret"]
                );

            _cloudinary = new Cloudinary(acc);
        }

        /// <summary>
        /// Dodanie obrazu do Cloudinary
        /// </summary>
        /// <param name="file">Obraz do przesłania jako plik IFormFile</param>
        /// <returns>Rezultat przesłania obrazu</returns>
        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                // Otwarcie strumienia
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                // Załadowanie pliku do Cloudinary
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        /// <summary>
        /// Usuwanie obrazu z Cloudinary
        /// </summary>
        /// <param name="publicId">Publiczne ID obrazu w Cloudinary</param>
        /// <returns>Rezultat usunięcia obrazu</returns> 
        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}