using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    // Klasa serwisu do zarządzania obrazami z użyciem Cloudinary.
    public class ImageService
    {
        private readonly Cloudinary _cloudinary;

        // Konstruktor inicjalizujący serwis z konfiguracją Cloudinary.
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
        /// Dodaje obraz do Cloudinary.
        /// </summary>
        /// <param name="file">Obraz do przesłania jako plik IFormFile.</param>
        /// <returns>Rezultat przesłania obrazu.</returns>
        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                // Otwarcie strumienia pliku.
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                // Przesłanie obrazu do Cloudinary.
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        /// <summary>
        /// Usuwa obraz z Cloudinary.
        /// </summary>
        /// <param name="publicId">Publiczne ID obrazu w Cloudinary.</param>
        /// <returns>Rezultat usunięcia obrazu.</returns>
        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}