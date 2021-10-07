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
            if (Transaction == null)
            {
                return NotFound();
            }

            return Transaction;
        }
        // GET: api/v1/get-anticipations/{0 || 1 || 2}
        [HttpGet("get-anticipations/{filter:int}")]
        public async Task<ActionResult<dynamic>> GetAnticipations([Range(0,2, ErrorMessage = "Value for {0} must be between {1} and {2}.")] int filter)
        {
            List<Anticipation> Anticipations = await _anticipationService.GetAnticipations(filter);
            if (Anticipations == null)
            {
                return NotFound();
            }

            return Anticipations;
        }

        [HttpPost("request-anticipation")]
        public async Task<ActionResult<dynamic>> RequestAnticipation(AnticipationModel model)
        {
            
            var anticipationProcessing = await _anticipationService.RequestAnticipation(model);

            if(anticipationProcessing == null)
            {
                return (new {ErrorMessage = "Request Failed" });
            }
            return (new {Message = "Request made successfully, ID: " + anticipationProcessing.Id});
        }
    }
}
