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
        private readonly TransactionService _transactionService;

        public TransactionController(AppDbContext context, TransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        // GET: api/v1/transaction/1
        [HttpGet("{transactionId:long}")]
        public async Task<ActionResult<Transaction>> Get([FromRoute]long transactionId)
        {
            Transaction Transaction = await _transactionService.GetTransaction(transactionId);

            return Transaction;
        }


        // POST: api/v1/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("pay-with-card")]
        public async Task<ActionResult<dynamic>> PayWithCard(PaymentModel model)
        {
            var payment = model.Map();
            
            var transactionProcessing = await _transactionService.Process(payment);

            if(transactionProcessing == null)
            {
                return NotFound("Failed Transaction");
            }
            if(!transactionProcessing.Acquirer)
            {
                return Ok("Transaction disapproved, ID" + transactionProcessing.Id);
            }
            return Ok("Approved transaction, ID: " + transactionProcessing.Id);
        }

    }
}
