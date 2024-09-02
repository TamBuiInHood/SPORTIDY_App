using System;
using System.ComponentModel.DataAnnotations;

public class AtLeastOneRequiredAttribute : ValidationAttribute
{
    private readonly string[] _properties;

    public AtLeastOneRequiredAttribute(params string[] properties)
    {
        _properties = properties;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        foreach (var property in _properties)
        {
            var propertyValue = validationContext.ObjectType.GetProperty(property)?.GetValue(validationContext.ObjectInstance, null);
            if (propertyValue != null)
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult("At least one of the properties must be provided.");
    }
}
