using BookingService;
using BookingService.Controllers;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web.Http.Results;

namespace BookingTest
{
    public class Tests
    {

        List<Booking> books = new List<Booking>();
        IQueryable<Booking> bookingdata;
        Mock<DbSet<Booking>> mockSet;
        Mock<BookingDbContext> bookcontextmock;
        [SetUp]
        public void Setup()
        {
            books = new List<Booking>()
            {
                new Booking{Booking_Id = 1, Ticket_Cost=300},
                 new Booking{Booking_Id = 2,Ticket_Cost=600}

            };
            bookingdata = books.AsQueryable();
            mockSet = new Mock<DbSet<Booking>>();
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Provider).Returns(bookingdata.Provider);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Expression).Returns(bookingdata.Expression);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.ElementType).Returns(bookingdata.ElementType);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.GetEnumerator()).Returns(bookingdata.GetEnumerator());
            var p = new DbContextOptions<BookingDbContext>();
            bookcontextmock = new Mock<BookingDbContext>(p);
            bookcontextmock.Setup(x => x.Bookings).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllBookingsByUserIdTest()
        {
            var bookingrepo = new BookingRepository(bookcontextmock.Object);
            var bookinglist = bookingrepo.GetById(1);
            Assert.AreEqual(2, bookinglist.Count());




        }
        [Test]
        public void AddBookingDetailTest()
        {
            var bookingrepository = new BookingRepository(bookcontextmock.Object);
            var bookingobject = bookingrepository.Book(new Booking { Booking_Id = 3, Ticket_Cost = 6000 });
            Assert.IsNotNull(bookingobject);
        }
    }
}