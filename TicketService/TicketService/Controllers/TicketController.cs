using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketService.Models;


namespace TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TicketController));
        private readonly ITicketRepository _ITicketRepository;
        public TicketController(ITicketRepository Repo)
        {
            this._ITicketRepository = Repo;
        }

        [HttpGet]

        public IActionResult GetAllTickets()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<Ticket> ticketlist = _ITicketRepository.GetAll();
                return Ok(ticketlist);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _log4net.Info(" Http GET by is accesed " + id);
                var ticketlist = _ITicketRepository.GetById(id);
                return new OkObjectResult(ticketlist);
            }
            catch
            {
                _log4net.Error("Error in Get by id Request");
                return new NoContentResult();
            }

        }

        [HttpPost]
        public IActionResult Post(Ticket ticket)
        {
            try
            {
                _log4net.Info("Book Details Getting Added");
                if (ModelState.IsValid)
                {

                    _ITicketRepository.AddTicket(ticket);

                    return CreatedAtAction(nameof(Post), new { id = ticket.Booking_Id }, ticket);


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
