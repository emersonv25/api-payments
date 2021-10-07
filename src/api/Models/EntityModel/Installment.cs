using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models.EntityModel
{
    public class Installment
    {

        public long Id { get; set; }
        public long TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime ForecastPaymentAt { get; set; }
        public decimal? AnticipatedAmount { get; set; }
        public DateTime? AnticipatedAt { get; set; }
        
    }
}