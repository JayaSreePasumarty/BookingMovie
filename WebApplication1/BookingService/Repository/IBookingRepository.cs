using BookingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService
{
    public interface IBookingRepository
    {

        IEnumerable<Booking> GetAll();
        IEnumerable<Booking> GetById(int id);

        Booking Book(Booking entity);
    }
}