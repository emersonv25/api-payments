using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models.EntityModel;
using api.Models.Validations;

namespace api.Models.ViewModel
{
    public class AnticipationModel
    {

        public decimal AmountRequest { get; set; }
        public List<TransactionIdModel> TransactionsList { get; set; }

    }
    public class TransactionIdModel
    {
        public long TransactionId { get; set; }

    }
}