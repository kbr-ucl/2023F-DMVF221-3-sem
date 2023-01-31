namespace Vejledningsbooking.Domain;

public class Student
{
    private List<Booking> _bookings = new();

    /// <summary>
    /// Hej med dig
    /// </summary>
    /// <remarks>
    /// Business rule: en Student må have max to bookings der starter i fremtiden
    /// </remarks>

    public List<Booking> Bookings
    {
        get { return new List<Booking>(_bookings); }

    }

    public void Add(Booking booking)
    {
        if (_bookings.Where(a => a.Start >= DateTime.Now).Count() >= 2)
            throw new Exception("Business rule: en Student må have max to bookings der starter i fremtiden");
        
        _bookings.Add(booking);
    }
}