using Jewbox.Models;
using Jewbox.Repositories;

namespace Jewbox.Services;

public class BookingService : IBookingService
{
    private readonly IUserRepository _userRepository;
    private const int WindowRangeMin = 15;

    public BookingService(IUserRepository  userRepository)
    {
        _userRepository = userRepository;
    }

    private static void Validate(DateTime date)
    {
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Monday:
            {
                //startDate = startDate.AddHours(12);
                //startDate = startDate.AddMinutes(30);
                break;
            }
            case DayOfWeek.Tuesday:
            {
                //startDate = startDate.AddHours(14);
                break;
            }
            case DayOfWeek.Wednesday:
            {
                //startDate = startDate.AddHours(8);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException("incorrect date set");
        }
    }

    public Booking GetBooking(int userId, BookingType type, DateTime start)
    {
        Validate(start);
        var user = _userRepository.GetUserById(userId);
        
        var result = new Booking
        {
            Person = user,
            Type = type,
            DesiredDate = start,
            EndDate = start.AddMinutes(WindowRangeMin),
        };
        
        return result;
    }
}