using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models.EntityModel
{
    public class Anticipation
    {

        public long Id { get; set; }
        public DateTime RequestAt { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? Result { get; set; } // 0 = disapproved | 1 = approved | 2 = partially approved
        public int Status { get; set; } // 0 = pending | 1 = under analysis | 2 = finished
        public decimal AmountRequest { get; set; }
        public decimal? AmountApproved { get; set; }
        public List<Transaction> TransactionsList { get; set; }
    }
}