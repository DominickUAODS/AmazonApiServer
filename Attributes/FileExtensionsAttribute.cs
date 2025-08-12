using System.ComponentModel.DataAnnotations;

namespace AmazonApiServer.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileExtensionsAttribute(string fileExtensions) : ValidationAttribute
    {
        private string[] AllowedExtensions { get; set; } = fileExtensions.Split([','], StringSplitOptions.RemoveEmptyEntries);

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file)
            {
                string fileName = file.FileName;
                return Array.Exists(AllowedExtensions, y => fileName.EndsWith('.' + y));
            }
            else if (value is IEnumerable<IFormFile> collection)
            {
                foreach (string fileName in collection.Select(f => f.FileName))
                {
                    if (!Array.Exists(AllowedExtensions, y => fileName.EndsWith('.' + y)))
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? $"The {name} field only accepts files with extensions: {string.Join(", ", AllowedExtensions.Select(str => '.' + str))}.";
        }
    }
}
