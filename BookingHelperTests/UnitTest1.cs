using NUnit.Framework;
using BookingHelper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests
{
    public class Tests
    {
        Mock<IBookingRepository> _repository;

        public Booking _existingBooking { get; private set; }

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IBookingRepository>();

            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 15),
                DepartureDate = new DateTime(2017, 1, 20),
                Reference = "a"
            };

            var list = new List<Booking>
            {
                _existingBooking
            };
            _repository.Setup(r => r.GetActiveBookings(It.IsAny<int>())).Returns(list.AsQueryable());
        }

        [Test]
        public void BookingTest_DateAfterBooking_StringEmpty()
        {
            var newbooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 10),
                DepartureDate = new DateTime(2017, 1, 12),
                Reference = "a"

            };
            var result = BookingsHelper.OverlappingBookingsExist(newbooking, _repository.Object);

            Assert.AreEqual(string.Empty, result);
        }
        [Test]
        public void BookingTest_DateStartingBeforeAndEndsDuring_ReturnsA()
        {
            var newbooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 10),
                DepartureDate = new DateTime(2017, 1, 16),
                Reference = "a"

            };
            var result = BookingsHelper.OverlappingBookingsExist(newbooking, _repository.Object);

            Assert.AreEqual("a", result);
        }
        [Test]
        public void BookingTest_DateStartingAndEndingDuringBooking_ReturnsA()
        {
            var newbooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 17),
                DepartureDate = new DateTime(2017, 1, 19),
                Reference = "a"

            };
            var result = BookingsHelper.OverlappingBookingsExist(newbooking, _repository.Object);

            Assert.AreEqual("a", result);
        }
        [Test]
        public void BookingTest_DateStartingDuringAndEndingAfter_ReturnsA()
        {
            var newbooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 19),
                DepartureDate = new DateTime(2017, 1, 16),
                Reference = "a"

            };
            var result = BookingsHelper.OverlappingBookingsExist(newbooking, _repository.Object);

            Assert.AreEqual("a", result);
        }
        [Test]
        public void BookingTest_DateStartingAfter_ReturnsEmptyString()
        {
            var newbooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 21),
                DepartureDate = new DateTime(2017, 1, 25),
                Reference = "a"

            };
            var result = BookingsHelper.OverlappingBookingsExist(newbooking, _repository.Object);

            Assert.AreEqual(string.Empty, result);
        }
        [Test]
        public void BookingTest_DateStartingBeforeAndEndingAfter_ReturnsA()
        {
            var newbooking = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 11),
                DepartureDate = new DateTime(2017, 1, 25),
                Reference = "a"

            };
            var result = BookingsHelper.OverlappingBookingsExist(newbooking, _repository.Object);

            Assert.AreEqual(string.Empty, result);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
        return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

    }
}