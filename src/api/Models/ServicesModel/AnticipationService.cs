using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.EntityModel;
using api.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace api.Models.ServicesModel
{
    public class AnticipationService
    {
        private readonly AppDbContext _context;
        public Anticipation anticipation { get; private set; }
        public AnticipationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Transaction>> GetTransactionAvailable()
        { 
            List<Transaction> Transaction = await _context.Transactions.Where(t => t.AnticipationId == null).ToListAsync();
            
            return Transaction;
        }
        public async Task<List<Anticipation>> GetAnticipations(int filter)
        { 
            List<Anticipation> Anticipations = new List<Anticipation>();
            switch (filter)
            {
                case 0:
                    Anticipations = await _context.Anticipations.Where(a => a.StartAt == null && a.EndAt == null).ToListAsync();
                break;

                case 1:
                    Anticipations = await _context.Anticipations.Where(a => a.StartAt != null && a.EndAt == null).ToListAsync();
                break;

                case 2:
                    Anticipations = await _context.Anticipations.Where(a => a.StartAt != null && a.EndAt != null).ToListAsync();
                break;
            }

            return Anticipations;
        }
        public async Task<Anticipation> RequestAnticipation(AnticipationModel request)
        {
            Anticipation anticipation = new Anticipation();
            return anticipation;

        }
    }
}