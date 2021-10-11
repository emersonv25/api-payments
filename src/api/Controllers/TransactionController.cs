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
using api.Models.ServicesModel.Interfaces;

namespace api.Controllers
{
    [Route("api/v1/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITransactionService _transactionService;

        public TransactionController(AppDbContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        // GET: api/v1/transaction/get/1
        /// <summary>
        ///     Retorna uma transação a partir de sua ID
        /// </summary>
        [HttpGet("{transactionId:long}")]
        public async Task<ActionResult<Transaction>> Get([FromRoute]long transactionId)
        {
            Transaction transaction = await _transactionService.GetTransaction(transactionId);
            if(transaction == null)
            {
                return NotFound("Transaction not found!");
            }

            return transaction;
        }


        // POST: api/v1/transaction/pay-with-card
        /// <summary>
        ///     Efetua um pagamento com cartão de crédito
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/v1/transaction/pay-with-card
        ///     {
        ///        "amount": 100,
        ///        "installmentsNumber": 2,
        ///        "creditCardNumber": "1234567891234567"
        ///     }
        ///
        /// </remarks>
        [HttpPost("pay-with-card")]
        public async Task<ActionResult> PayWithCard([FromBody]PaymentModel model)
        {
            var payment = model.Map();
            
            var transactionProcessing = await _transactionService.Process(payment);

            if(transactionProcessing == null)
            {
                return NotFound("Failed Transaction");
            }
            if(!transactionProcessing.Acquirer)
            {
                return Ok("Transaction disapproved, ID " + transactionProcessing.Id);
            }
            return Ok(transactionProcessing);
        }

    }
}
