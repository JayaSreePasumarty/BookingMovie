using BookingService;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext bookingDbContext;

        public BookingRepository(BookingDbContext bookingDbContext)
        {
            this.bookingDbContext = bookingDbContext;
        }
        public IEnumerable<Booking> GetAll()
        {
            var booklist = bookingDbContext.Bookings.Include(r => r).ToList();
            return booklist;
        }

        //   public Booking GetById(int userid)
        public IEnumerable<Booking> GetById(int id)
        {

            return bookingDbContext.Bookings.Where(b => b.Booking_Id == id).ToList();
        }
        public Booking Book(Booking booking)
        {


            var result = bookingDbContext.Bookings.Add(booking);
            bookingDbContext.SaveChanges();
            return booking;


            // return result.Entity;

        }

       
    }
}