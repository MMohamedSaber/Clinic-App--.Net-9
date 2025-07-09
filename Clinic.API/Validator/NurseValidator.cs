using Clinic.Core.DTOs;
using FluentValidation;

namespace Clinic.API.Validator
{
    public class NurseValidator : AbstractValidator<UpdateNurseDto>
    {
        public NurseValidator()
        {
            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .MaximumLength(50).WithMessage("Specialization must be less than 50 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(30).WithMessage("Role must be less than 30 characters.");

            RuleFor(x => x.No_Of_Hour)
                .InclusiveBetween(1, 168).WithMessage("Hours must be between 1 and 168.");

            RuleFor(x => x.Shift)
                .NotEmpty().WithMessage("Shift is required.")
                .MaximumLength(30).WithMessage("Shift must be less than 30 characters.");

            RuleFor(x => x.Qualifications)
                .NotEmpty().WithMessage("Qualifications are required.")
                .MaximumLength(100).WithMessage("Qualifications must be less than 100 characters.");

            RuleFor(x => x.dept_id)
                .GreaterThan(0).WithMessage("Department ID must be greater than 0.");

        }
    }
}
