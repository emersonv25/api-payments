using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models.EntityModel
{
    public class Installment
    {

        public long InstallmentId { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public int InstallmentsNumber { get; set; }
        public decimal AnticipatedAmount { get; set; }
        public DateTime ForecastPaymentAt { get; set; }
        public DateTime PaymentAt { get; set; }
        public long TransactionId { get; set; }
        public Transaction Transaction { get; set;}

        

    }
}