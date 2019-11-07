using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingHelper
{
    class BookingRepository:IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            unitOfWork.Query<Booking>()
                 .Where(
            b => b.Id != b.Id && b.Status != "Cancelled");

            throw new NotImplementedException();
        }
    }
}
