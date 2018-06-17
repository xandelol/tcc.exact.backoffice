using System;
using System.ComponentModel.DataAnnotations;

namespace exac.backoffice.Models
{
    public class SettingViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo obrigátório.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Campo obrigátório.")]
        [SettingValue]
        public string Value { get; set; }
        
        public string Key { get; set; }
        
        public int Type { get; set; }
    }

    public class SettingValueAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var settingViewModel = (SettingViewModel) validationContext.ObjectInstance;

            switch (settingViewModel.Type)
            {
                 case 0:
                     int resultInt;
                     if (int.TryParse(settingViewModel.Value, out resultInt))
                     {
                         return ValidationResult.Success;
                     }
                     return new ValidationResult("Valor precisa ser um número inteiro.");
                case 1:
                    return ValidationResult.Success;
                case 2:
                    double resultDouble;
                    if (double.TryParse(settingViewModel.Value, out resultDouble))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("Valor precisa ser um número.");
                case 3:
                    DateTime resultDate;
                    if (DateTime.TryParse(settingViewModel.Value, out resultDate))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("Valor precisa ser uma data válida.");
            }

            return ValidationResult.Success;
        }

    }
}