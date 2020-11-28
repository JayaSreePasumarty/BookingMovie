using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TicketService.Models
{
    public class TicketModelDbContext : DbContext
    {
        public TicketModelDbContext(DbContextOptions<TicketModelDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}