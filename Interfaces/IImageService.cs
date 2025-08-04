namespace AmazonApiServer.Interfaces
{
	public interface IImageService
	{
		Task<string> UploadAsync(IFormFile file);
		Task<bool> DeleteAsync(string url);
	}
}
