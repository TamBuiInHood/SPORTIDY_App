using System.ComponentModel.DataAnnotations;
using System.IO;

public class FileFormatAttribute : ValidationAttribute
{
    private readonly string[] _formats;

    public FileFormatAttribute(params string[] formats)
    {
        _formats = formats;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return ValidationResult.Success;
        }

        var fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension == null || !_formats.Contains(fileExtension.ToLower()))
        {
            return new ValidationResult($"Invalid file format. Allowed formats are: {string.Join(", ", _formats)}");
        }

        return ValidationResult.Success;
    }
}
