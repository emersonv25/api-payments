using System.ComponentModel.DataAnnotations;
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
            if(cardNumber.Length < 16)
            {
                return new ValidationResult(GetErrorMessage());
            }
            int num;
            if(int.TryParse(cardNumber, out num))
            {
                return new ValidationResult(GetErrorMessage());
            }
            if(cardNumber.Substring(0, 5) == "5999")
            {
                return new ValidationResult(GetErrorMessage());;
            }
            return ValidationResult.Success;
        }
    }
}