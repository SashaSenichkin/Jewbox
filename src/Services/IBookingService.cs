using Jewbox.Models;

namespace Jewbox.Services;

public interface IBookingService
{
    public Booking GetBooking(int userId, BookingType type, DateTime time);
}