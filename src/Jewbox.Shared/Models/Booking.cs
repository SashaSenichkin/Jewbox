namespace Jewbox.Models;

public class Booking
{
    public BookingType Type { get; init; }
    public DateTime DesiredDate { get; init; }
    public DateTime? EndDate { get; set; }
    public required User Person { get; init; }
}
