using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TicketService.Models
{
    public class Ticket
    {
        [Key]
        public int Booking_Id { get; set; }
        public int User_Id { get; set; }
        public int Ticket_Id { get; set; }
        public DateTime BookingDate { get; set; }

    }

}