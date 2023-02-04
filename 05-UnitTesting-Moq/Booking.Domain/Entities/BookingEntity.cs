using Booking.Domain.DomainServices;

namespace Booking.Domain.Entities;

public class BookingEntity
{
    private readonly IBookingDomainService _bookingDomainService;
    private readonly DateTime _now;

    public BookingEntity(IBookingDomainService bookingDomainService, DateTime startDateTime, DateTime endDateTime)
    {
        _bookingDomainService = bookingDomainService;
        _now = _bookingDomainService.GetCurrentDateTime();
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        ValidateBooking();
    }

    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }

    private void ValidateBooking()
    {
        ValidateIsSetAndInFuture(nameof(StartDateTime), StartDateTime);
        ValidateIsSetAndInFuture(nameof(EndDateTime), EndDateTime);

        // End date later than start date
        if (EndDateTime <= StartDateTime)
            throw new ArgumentException($"{nameof(EndDateTime)} must be later than {nameof(StartDateTime)}");

        // No overlap
        if (_bookingDomainService.IsOverlapping(this)) throw new Exception("Booking is overlapping exsisting bookings");
    }

    private void ValidateIsSetAndInFuture(string parameter, DateTime date)
    {
        if (date == default) throw new ArgumentException($"{parameter} is not set");
        if (date <= _now) throw new ArgumentException($"{parameter} must be in the future");
    }
}