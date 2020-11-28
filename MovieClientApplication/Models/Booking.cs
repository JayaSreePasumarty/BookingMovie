using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieClientApplication.Models
{
    public class Booking
    {
        [Key]
        public int Booking_Id { get; set; }
        public int Ticket_Cost { get; set; }
    }
}
