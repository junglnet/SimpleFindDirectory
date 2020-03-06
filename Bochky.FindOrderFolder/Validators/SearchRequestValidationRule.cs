using System.Globalization;
using System.Windows.Controls;

namespace Bochky.FindDirectory.Validators
{
    public class SearchRequestValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                return new ValidationResult(false, "Значение не может быть пустым");

            else if (value.ToString().Length < 4)
                return new ValidationResult(false, "Строка не может быть меньше трех символов");

            else
                return ValidationResult.ValidResult;


        }
    }
}
