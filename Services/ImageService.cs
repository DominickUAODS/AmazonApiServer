using AmazonApiServer.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace AmazonApiServer.Services
{
    public class ImageService(IConfiguration configuration): IImageService
    {
        private readonly Cloudinary cloudinary = new(configuration.GetValue<string>("CLOUDINARY_URL"));
        public async Task<string> UploadAsync(IFormFile file)
        {
            await using Stream stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams { File = new FileDescription(file.FileName, stream) };
            ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }

        public async Task<bool> DeleteAsync(string url)
        {
            string publicId = GetPublicIdFromUrl(url);
            DeletionParams deletionParams = new(publicId);
            DeletionResult result = await cloudinary.DestroyAsync(deletionParams);
            Console.WriteLine(result.Result);
            return result.Result == "ok";
        }

        private static string GetPublicIdFromUrl(string url)
        {
            return Path.GetFileNameWithoutExtension(url);
        }
    }
}
