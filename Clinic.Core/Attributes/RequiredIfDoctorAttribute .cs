using System.ComponentModel.DataAnnotations;

namespace Clinic.Core.Attributes
{
    public class RequiredIfDoctorAttribute : ValidationAttribute
    {
        private readonly string _userTypePropertyName;

        public RequiredIfDoctorAttribute(string userTypePropertyName)
        {
            _userTypePropertyName = userTypePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userTypeProperty = validationContext.ObjectType.GetProperty(_userTypePropertyName);
            if (userTypeProperty == null)
                return new ValidationResult($"Unknown property: {_userTypePropertyName}");

            var userTypeValue = userTypeProperty.GetValue(validationContext.ObjectInstance)?.ToString();

            if (userTypeValue == "Doctor" && string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult("Specialization is required for doctors.");
            }

            return ValidationResult.Success;
        }
    }
}