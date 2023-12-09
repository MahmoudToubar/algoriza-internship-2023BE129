using System.ComponentModel.DataAnnotations;

namespace VezeetaManagement.Helper
{
    public class CheckAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public CheckAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOfBirth = Convert.ToDateTime(value);

            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now < dateOfBirth.AddYears(age))
            {
                age--;
            }

            if (age < _minAge)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
