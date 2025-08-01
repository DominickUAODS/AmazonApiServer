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
            return result.Result == "ok" || result.Result == "deleted";
        }

        private static string GetPublicIdFromUrl(string url)
        {
            Uri uri = new(url);
            string[] pathSegments = uri.AbsolutePath.Split('/');
            string publicIdWithExtension = string.Join('/', pathSegments.Skip(4));
            return Path.Combine(Path.GetDirectoryName(publicIdWithExtension) ?? "", Path.GetFileNameWithoutExtension(publicIdWithExtension)).Replace("\\", "/");
        }
    }
}
