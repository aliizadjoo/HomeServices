using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.DataAnnotations
{
    public class MinPriceBasedOnAttribute : ValidationAttribute
    {
        private readonly string _basePropertyName;

        public MinPriceBasedOnAttribute(string basePropertyName)
        {
            _basePropertyName = basePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var baseProperty = validationContext.ObjectType.GetProperty(_basePropertyName);

            if (baseProperty == null)
                return new ValidationResult($"ویژگی {_basePropertyName} یافت نشد.");

            var baseValue = (decimal)baseProperty.GetValue(validationContext.ObjectInstance);
            var priceValue = (decimal)value;

            if (priceValue < baseValue)
            {
                return new ValidationResult(ErrorMessage ?? $"مبلغ نمی‌تواند کمتر از {baseValue:N0} باشد.");
            }

            return ValidationResult.Success;
        }
    }
}
