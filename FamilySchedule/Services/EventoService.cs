using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using FamilySchedule.Models;
using FamilySchedule.Models.Context;
using Microsoft.EntityFrameworkCore;


namespace FamilySchedule.Models.Services
{
    public class EventoService 
    {
        private readonly ApplicationDbContext _context;

        public EventoService(ApplicationDbContext context)
        {
            context = _context;
        }

        public async Task<List<EventoModel>> GetEventosAsync()
        {
            return await _context.Eventos.ToListAsync();
        }
    }
}
