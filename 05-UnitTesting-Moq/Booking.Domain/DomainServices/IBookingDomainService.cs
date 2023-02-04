using Booking.Domain.Entities;

namespace Booking.Domain.DomainServices;

public interface IBookingDomainService
{
    bool IsOverlapping(BookingEntity changedBooking);
    DateTime GetCurrentDateTime();
}