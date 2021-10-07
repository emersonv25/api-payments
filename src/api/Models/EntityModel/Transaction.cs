using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models.EntityModel
{
    public class Transaction
    {

        public long Id { get; set; }
        public DateTime TransactionAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? DisapprovedAt { get; set; }
        public bool? Anticipated { get; set;}
        public bool Acquirer { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Fee { get; set; } 
        public int InstallmentsNumber { get; set; }
        public string LastFourDigitsCard { get; set; }
        public List<Installment> Installments { get; set; }

        [NotMapped]
        public string CreditCardNumber { get; set; }

    }
}