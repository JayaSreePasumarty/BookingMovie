using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Models
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(int ticketid);

        Ticket AddTicket(Ticket entity);



    }
}