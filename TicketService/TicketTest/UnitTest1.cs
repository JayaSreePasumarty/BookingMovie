using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TicketService;
using TicketService.Controllers;
using TicketService.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TicketTest
{
    public class Tests
    {

        List<Ticket> ticket = new List<Ticket>();
        IQueryable<Ticket> ticketdata;
        Mock<DbSet<Ticket>> mockSet;
        Mock<TicketModelDbContext> pgcontextmock;
        [SetUp]
        public void Setup()
        {
            ticket = new List<Ticket>
            {
                new Ticket{Booking_Id = 1, User_Id=1, Ticket_Id=1,BookingDate=DateTime.Parse("18-11-2020 00:00:00")},
                new Ticket{Booking_Id = 1, User_Id=1, Ticket_Id=2,BookingDate=DateTime.Parse("18-11-2020 00:00:00")},
                new Ticket{Booking_Id = 1, User_Id=1, Ticket_Id=3,BookingDate=DateTime.Parse("18-11-2020 00:00:00")}
            };
            ticketdata = ticket.AsQueryable();
            mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(ticketdata.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(ticketdata.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(ticketdata.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(ticketdata.GetEnumerator());
            var p = new DbContextOptions<TicketModelDbContext>();
            pgcontextmock = new Mock<TicketModelDbContext>(p);
            pgcontextmock.Setup(x => x.Tickets).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllTest()
        {
            var ticketrepository = new TicketRepository(pgcontextmock.Object);
            var ticketlist = ticketrepository.GetAll();
            Assert.AreEqual(3, ticketlist.Count());




        }
        [Test]
        public void GetByIdTest()
        {
            var ticketrepository = new TicketRepository(pgcontextmock.Object);
            var ticketobject = ticketrepository.GetById(2);
            Assert.IsNotNull(ticketobject);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var ticketrepository = new TicketRepository(pgcontextmock.Object);
            var ticketobject = ticketrepository.GetById(88);
            Assert.IsNull(ticketobject);
        }

    }

}
/*using TicketService;
using TicketService.Controllers;
using TicketService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web.Http.Results;

namespace TicketTest
{
    public class Tests
    {

        List<Ticket> tickets = new List<Ticket>();
        IQueryable<Ticket> bookingdata;
        Mock<DbSet<Ticket>> mockSet;
        Mock<TicketModelDbContext> bookcontextmock;
        [SetUp]
        public void Setup()
        {
            tickets = new List<Ticket>()
            {
                new Ticket{Booking_Id = 1, User_Id=1, Ticket_Id=1,BookingDate=DateTime.Parse("18-11-2020 00:00:00")},
                 new Ticket{Booking_Id = 2, User_Id=2, Ticket_Id=2,BookingDate=DateTime.Parse("28-11-2020 00:00:00")}

            };
            bookingdata = tickets.AsQueryable();
            mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(bookingdata.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(bookingdata.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(bookingdata.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(bookingdata.GetEnumerator());
            var p = new DbContextOptions<TicketModelDbContext>();
            bookcontextmock = new Mock<TicketModelDbContext>(p);
            bookcontextmock.Setup(x => x.Tickets).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllTicketsByUserIdTest()
        {
            var ticketrepository = new TicketRepository(bookcontextmock.Object);
            var ticketlist = ticketrepository.GetById(1);
            Assert.AreEqual(2, ticketlist.Count());




        }
        [Test]
        public void AddTicketDetailTest()
        {
            var ticketrepository = new TicketRepository(bookcontextmock.Object);
            var ticketobject = ticketrepository.Ticket(new Ticket { Booking_Id = 3, User_Id = 3, Ticket_Id = 3, BookingDate = DateTime.Parse("04-12-2020 00:00:00") });
            Assert.IsNotNull(ticketobject);
        }
    }
}*/