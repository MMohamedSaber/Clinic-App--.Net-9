using Clinic.Core.DTOs;
using FluentValidation;

namespace Clinic.API.Validator
{
    public class UserValidator : AbstractValidator<RegisterDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .Length(3, 50).WithMessage("Full name must be between 3 and 50 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .Must(BeAValidDate).WithMessage("Invalid date format. Use yyyy-MM-dd.");

            RuleFor(x => x.Blood_Type)
                .NotEmpty().WithMessage("Blood type is required.")
                .Matches("^(A|B|AB|O)[+-]$").WithMessage("Blood type must be A+, A-, B+, B-, AB+, AB-, O+ or O-");

            RuleFor(x => x.Governorate)
                .NotEmpty().WithMessage("Governorate is required.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female").WithMessage("Gender must be either 'Male' or 'Female'.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^01[0125]\d{8}$").WithMessage("Phone number must be a valid Egyptian number.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }
    }
}
