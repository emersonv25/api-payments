using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models.EntityModel;
using api.Models.Validations;

namespace api.Models.ViewModel
{
    public class PaymentModel
    {
        [Required (ErrorMessage = "Required Field")]
        public decimal Amount { get; set; }

        [Required (ErrorMessage = "Required Field")]
        public int InstallmentsNumber { get; set; }

        [CreditCardValid]
        public string CreditCardNumber { get; set; }

        public Transaction Map() => new Transaction
        {
            Amount = Amount,
            TransactionAt = DateTime.Now,
            InstallmentsNumber = InstallmentsNumber,
            LastFourDigitsCard = CreditCardNumber.Substring(CreditCardNumber.Length -4),
            CreditCardNumber = CreditCardNumber
        };

    }
}
