using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Models;

namespace TicketService.Models
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketModelDbContext TicketDbContext;

        public TicketRepository(TicketModelDbContext ticketDbContext)
        {
            this.TicketDbContext = ticketDbContext;
        }

        public IEnumerable<Ticket> GetAll()
        {
            var ticketlist = TicketDbContext.Tickets.Include(r => r).ToList();
            return ticketlist;
        }
        public Ticket GetById(int Ticket_Id)
        {
            return TicketDbContext.Tickets.FirstOrDefault(p => p.Ticket_Id == Ticket_Id);
        }

        public Ticket AddTicket(Ticket ticket)
        {


            var result = TicketDbContext.Tickets.Add(ticket);
            TicketDbContext.SaveChanges();
            return ticket;
            // return result.Entity;

        }


    }
}