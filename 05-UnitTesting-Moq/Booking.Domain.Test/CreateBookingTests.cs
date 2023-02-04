using Booking.Domain.DomainServices;
using Booking.Domain.Entities;
using Moq;

namespace Booking.Domain.Test;

public class CreateBookingTests
{
    [Theory]
    [InlineData("19-02-2023", "20-02-2023", "21-02-2023")]
    [InlineData("19-01-2023", "20-01-2023", "21-01-2023")]
    public void Given_parameters_are_valid__Then_Booking_created(string srtNow, string strStart, string strSlut)
    {
        // Arrange
        var now = DateTime.Parse(srtNow);
        var start = DateTime.Parse(strStart);
        var end = DateTime.Parse(strSlut);

        //https://github.com/moq/moq4
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(service => service.IsOverlapping(It.IsAny<BookingEntity>())).Returns(false);
        bookingDomainServiceMock.Setup(service => service.GetCurrentDateTime()).Returns(now);

        // Act
        var sut = new BookingEntity(bookingDomainServiceMock.Object, start, end);

        // Assert
        Assert.NotNull(sut);
    }

    [Fact]
    public void Given_overlap__Then_Exception()
    {
        // Arrange
        var now = DateTime.Parse("19-02-2023");
        var start = DateTime.Parse("20-02-2023");
        var end = DateTime.Parse("21-02-2023");

        //https://github.com/moq/moq4
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(service => service.IsOverlapping(It.IsAny<BookingEntity>())).Returns(true);
        bookingDomainServiceMock.Setup(service => service.GetCurrentDateTime()).Returns(now);

        // Act & Assert
        Assert.Throws<Exception>(() => new BookingEntity(bookingDomainServiceMock.Object, start, end));
    }

    [Fact]
    public void Given_start_before_now__Then_Exception()
    {
        // Arrange
        var now = DateTime.Parse("19-02-2023");
        var start = DateTime.Parse("18-02-2023");
        var end = DateTime.Parse("21-02-2023");

        //https://github.com/moq/moq4
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(service => service.IsOverlapping(It.IsAny<BookingEntity>())).Returns(true);
        bookingDomainServiceMock.Setup(service => service.GetCurrentDateTime()).Returns(now);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BookingEntity(bookingDomainServiceMock.Object, start, end));
    }

    [Fact]
    public void Given_end_before_now__Then_Exception()
    {
        // Arrange
        var now = DateTime.Parse("19-02-2023");
        var start = DateTime.Parse("17-02-2023");
        var end = DateTime.Parse("18-02-2023");

        //https://github.com/moq/moq4
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(service => service.IsOverlapping(It.IsAny<BookingEntity>())).Returns(true);
        bookingDomainServiceMock.Setup(service => service.GetCurrentDateTime()).Returns(now);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BookingEntity(bookingDomainServiceMock.Object, start, end));
    }

    [Fact]
    public void Given_end_before_start__Then_Exception()
    {
        // Arrange
        var now = DateTime.Parse("19-02-2023");
        var start = DateTime.Parse("28-02-2023");
        var end = DateTime.Parse("27-02-2023");

        //https://github.com/moq/moq4
        var bookingDomainServiceMock = new Mock<IBookingDomainService>();
        bookingDomainServiceMock.Setup(service => service.IsOverlapping(It.IsAny<BookingEntity>())).Returns(true);
        bookingDomainServiceMock.Setup(service => service.GetCurrentDateTime()).Returns(now);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BookingEntity(bookingDomainServiceMock.Object, start, end));
    }
}