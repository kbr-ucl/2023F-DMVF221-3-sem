using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vejledningsbooking.Domain.Test.StudentTests
{
    public class StudentBookingTests
    {
        [Fact]
        public void Given_Less_Than_Two_Bookings_Exsists__And_Add_Booking__Then_Booking_Added()
        {
            // Arrange
            var sut = new Student();
            // Act
            sut.Add(new Booking(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));


            // Assert
            Assert.Equal(1, sut.Bookings.Count());
        }

        [Fact]
        public void Given__Two_Bookings_Exsists_One_Is_Old__And_Add_Booking__Then_Booking_Added()
        {
            // Arrange
            var sut = new Student();
            sut.Add(new Booking(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));
            sut.Add(new Booking(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1)));
            // Act
            sut.Add(new Booking(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4)));
            //Assert
            Assert.Equal(3, sut.Bookings.Count);
        }

        [Fact]
        public void Given__Two_Bookings_Exsists__And_Add_Booking__Then_Exception()
        {
            // Arrange
            var sut = new Student();
            sut.Add(new Booking(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));
            sut.Add(new Booking(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1)));


            // Act & Assert
            Assert.Throws<Exception>(() => sut.Add(new Booking(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4))));
        }
    }
}
