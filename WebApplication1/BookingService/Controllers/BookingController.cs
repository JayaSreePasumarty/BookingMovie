using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BookingService.Models;
using BookingService.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));
        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        [HttpGet]

        public IActionResult GetAllBookings()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<Booking> ticketlist = _bookingRepository.GetAll();
                return Ok(ticketlist);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                _log4net.Info("Get BookingDetails by Id accessed");
                var bookinglist = _bookingRepository.GetById(id);
                return new OkObjectResult(bookinglist);

            }
            catch
            {
                _log4net.Error("Error in getting Booking Details");
                return new NoContentResult();
            }
        }
        [HttpPost]
        public IActionResult Post(Booking booking)
        {
            try
            {
                _log4net.Info("Book Details Getting Added");
                if (ModelState.IsValid)
                {

                    _bookingRepository.Book(booking);

                    return CreatedAtAction(nameof(Post), new { id = booking.Booking_Id }, booking);

                }
                return BadRequest();


            }
            catch
            {
                _log4net.Error("Error in Adding Booking Details");
                return new NoContentResult();
            }

        }
    }
}
