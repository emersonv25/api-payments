using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models.EntityModel
{
    public class Transaction
    {

        public long TransactionId { get; set; }
        public DateTime ApprovedAt { get; set; }
        public DateTime? DisapprovedAt { get; set; }
        public bool PreAnticipation { get; set;}
        public bool Acquirer { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Fee { get; set; } 
        public int InstallmentsNumber { get; set; }
        public int LastFourDigitsCard { get; set; }
        public List<Installment> Installment { get; set; }
    }
}