using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using api.Models.ViewModel;

namespace api.Models.Validations
{
    public class CreditCardValid : ValidationAttribute
    {
        public string GetErrorMessage() => $"Credit Card Number is Invalid.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var payment = (PaymentModel)validationContext.ObjectInstance;
            var cardNumber = payment.CreditCardNumber;
            if(cardNumber.Length != 16)
            {
                return new ValidationResult($"A credit card number must have 16 digits");
            }

            if(Regex.IsMatch(cardNumber, @"^[a-zA-Z]+$"))
            {
                return new ValidationResult($"Credit Card Field can only contain numbers.");
            }
            return ValidationResult.Success;
        }
    }
}
