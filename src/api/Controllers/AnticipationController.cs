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
using System.ComponentModel.DataAnnotations;

namespace api.Controllers
{
    [Route("api/v1/anticipations")]
    [ApiController]
    public class AnticipationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AnticipationService _anticipationService;

        public AnticipationController(AppDbContext context, AnticipationService anticipationService)
        {
            _context = context;
            _anticipationService = anticipationService;
        }

        // GET: api/v1/anticipation/get-transaction-available
        [HttpGet("get-transaction-available")]
        public async Task<ActionResult<dynamic>> GetTransactionAvailable()
        {
            List<Transaction> Transaction = await _anticipationService.GetTransactionAvailable();
            return Transaction;
        }
        // GET: api/v1/get-anticipations/{0 || 1 || 2}
        [HttpGet("get-anticipations/{filter:int}")]
        public async Task<ActionResult<dynamic>> GetAnticipations([Range(0,2, ErrorMessage = "Value for {0} must be between {1} and {2}.")] int filter)
        {
            List<Anticipation> Anticipations = await _anticipationService.GetAnticipations(filter); 
            return Anticipations;
        }
        // post: api/v1/anticipation/request-anticipation/ 
        //FromBody: [{transactionId:long}, {transactionId:long}]
        [HttpPost("request-anticipation")]
        public async Task<ActionResult> RequestAnticipation(List<long> transactionIds)
        {
            
            var anticipationProcessing = await _anticipationService.RequestAnticipation(transactionIds);

            if(anticipationProcessing == null)
            {
                return NotFound("Request Failed");
            }
            return Ok("Anticipation requested successfully");
        }

        // POST: api/v1/analysis/start/{anticipationId:long}
        [HttpPost("analysis/start/{anticipationId:long}")]
        public async Task<ActionResult> StartAnticipation(long anticipationId)
        {
            
            var anticipationProcessing = await _anticipationService.StartAnticipation(anticipationId);

            if(anticipationProcessing == null)
            {
                return NotFound("Anticipation not found or already started!");
            }
            return Ok("Analysis started successfully");
        }

        // GET: api/v1/analysis/approve/
        //FromBody: [{transactionId:long}, {transactionId:long}]
        [HttpPost("analysis/approve/")]
        public async Task<ActionResult> ApproveAnticipation(List<long> transactionIds)
        {
            
            var anticipationProcessing = await _anticipationService.ApproveAnticipation(transactionIds);

            if(anticipationProcessing == null)
            {
                return NotFound("Request Failed");
            }
            return Ok("Transactions successfully Approved!");
        }
        // GET: api/v1/analysis/disapprove/
        //FromBody: [{transactionId:long}, {transactionId:long}]
        [HttpPost("analysis/disapprove/")]
        public async Task<ActionResult> DisapproveAnticipation(List<long> transactionIds)
        {
            
            var anticipationProcessing = await _anticipationService.DisapproveAnticipation(transactionIds);

            if(anticipationProcessing == null)
            {
                return NotFound("Request Failed");
            }
            return Ok("Transactions successfully Disapproved!");
        }
    }
}
