namespace Vejledningsbooking.Domain;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Business rule: en Booking Student må have max to bookings der starter i fremtiden
/// </remarks>
public class Booking
{
    public Timeslot Timeslot { get; set; }
    public Student Student { get; set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public Booking(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

}