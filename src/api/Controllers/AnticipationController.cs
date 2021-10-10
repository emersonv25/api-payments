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
using api.Models.ServicesModel.Interfaces;

namespace api.Controllers
{
    [Route("api/v1/anticipations")]
    [ApiController]
    public class AnticipationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAnticipationService _anticipationService;

        public AnticipationController(AppDbContext context, IAnticipationService anticipationService)
        {
            _context = context;
            _anticipationService = anticipationService;
        }

        // GET: api/v1/anticipation/get-transaction-available
        /// <summary>
        ///     Consultar transações disponíveis para solicitar antecipação
        /// </summary>
        [HttpGet("transaction-available")]
        public async Task<ActionResult<dynamic>> GetTransactionAvailable()
        {
            List<Transaction> Transaction = await _anticipationService.GetTransactionAvailable();
            return Transaction;
        }

        /// <summary>
        ///     Consultar histórico de antecipações com filtro por status (0 = pendente, 1 = em análise, 2 = finalizada).
        /// </summary>
        // GET: api/v1/get-anticipations/{0 || 1 || 2}
        [HttpGet("find-anticipations/{filter:int}")]
        public async Task<ActionResult<dynamic>> GetAnticipations([Range(0,2, ErrorMessage = "Value for {0} must be between {1} and {2}.")] int filter)
        {
            List<Anticipation> Anticipations = await _anticipationService.GetAnticipations(filter); 
            return Anticipations;
        }

        // POST: api/v1/anticipation/request-anticipation/ 
        /// <summary>
        ///     Solicitar antecipação a partir de uma lista com os Ids das transações;
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/v1/anticipations/request-anticipation
        ///     [
        ///       1,2,3
        ///     ]
        /// </remarks>
        [HttpPost("request-anticipation")]
        public async Task<ActionResult> RequestAnticipation(List<long> transactionIds)
        {
            Anticipation anticipation = new Anticipation();
            try
            {
                var anticipationProcessing = await _anticipationService.RequestAnticipation(transactionIds);
                if(anticipationProcessing == null)
                {
                    return NotFound("Request Failed");
                }
                return Ok(anticipation);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/v1/analysis/start/{anticipationId:long}
        /// <summary>
        ///     Iniciar o atendimento da antecipação;
        /// </summary>
        [HttpPut("analysis/start/{anticipationId:long}")]
        public async Task<ActionResult> StartAnticipation(long anticipationId)
        {
            try
            {
                var anticipationProcessing = await _anticipationService.StartAnticipation(anticipationId);

                if(anticipationProcessing == null)
                {
                    return NotFound();
                }
                return Ok("Analysis started successfully");            
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/v1/analysis/approve/
        //FromBody: [{transactionId:long}, {transactionId:long}]
        /// <summary>
        ///     Aprovar uma ou mais transações da antecipação a partir de uma lista com os Ids das transações;
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/v1/analysis/approve/
        ///     [
        ///       1,2,3
        ///     ]
        /// </remarks>
        [HttpPut("analysis/approve/")]
        public async Task<ActionResult> ApproveAnticipation(List<long> transactionIds)
        {
            try
            {
                var anticipationProcessing = await _anticipationService.ApproveAnticipation(transactionIds);

                if(anticipationProcessing == null)
                {
                    return NotFound("Request Failed");
                }
                return Ok("Transactions successfully Approved!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // GET: api/v1/analysis/disapprove/
        //FromBody: [{transactionId:long}, {transactionId:long}]
        /// <summary>
        ///     Reprovar uma ou mais transações da antecipação a partir de uma lista com os Ids das transações;
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/v1/analysis/disapprove/
        ///     [
        ///       1,2,3
        ///     ]
        /// </remarks>
        [HttpPut("analysis/disapprove/")]
        public async Task<ActionResult> DisapproveAnticipation(List<long> transactionIds)
        {          
            try
            {
                var anticipationProcessing = await _anticipationService.DisapproveAnticipation(transactionIds);

                if(anticipationProcessing == null)
                {
                    return NotFound("Request Failed");
                }
                return Ok("Transactions successfully disapproved!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
