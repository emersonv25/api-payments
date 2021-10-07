using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models.EntityModel;
using Microsoft.AspNetCore.Authorization;
using api.Models.Validations;
using api.Models.ServicesModel;
using api.Models.ViewModel;
using Newtonsoft.Json;

namespace api.Controllers
{
    [Route("api/v1/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TransactionProcessing _transactionProcessing;

        public TransactionController(AppDbContext context, TransactionProcessing transactionProcessing)
        {
            _context = context;
            _transactionProcessing = transactionProcessing;
        }

        // GET: api/v1/transaction/1
        [HttpGet("{transactionId:long}")]
        public async Task<ActionResult<Transaction>> Find([FromRoute]long transactionId)
        {
            Transaction Transaction = await _transactionProcessing.FindTransaction(transactionId);
            if (Transaction == null)
            {
                return NotFound();
            }

            return Transaction;
        }


        // POST: api/v1/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("pay-with-card")]
        public async Task<ActionResult<dynamic>> PayWithCard(PaymentModel model)
        {
            var payment = model.Map();
            
            var transactionProcessing = await _transactionProcessing.Process(payment);

            if(transactionProcessing == null)
            {
                return (new {ErrorMessage = "Failed Transaction" });
            }
            if(!transactionProcessing.Acquirer)
            {
                return (new {ErrorMessage = "Transaction disapproved, ID" + transactionProcessing.Id});
            }

            return (new {Message = "Approved transaction, ID:" + transactionProcessing.Id});
        }

    }
}
