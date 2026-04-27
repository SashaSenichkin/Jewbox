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

    public bool IsCorrectDate(DateTime? date)
    {
        if (date == null)
        {
            return false;
        }

        try
        {
            Validate(date.Value);
        }
        catch (Exception)
        {
            return false;
        }
        
        return true;
    }

    private static void Validate(DateTime date)
    {
        if (date.Minute % 15 != 0)
        {
            throw new ArgumentException("Minute have to be multiple of 15");
        }

        TimeSpan start, end;
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Monday:
                start = new TimeSpan(12, 30, 0);
                end = new TimeSpan(16, 0, 0);
                break;
            case DayOfWeek.Tuesday:
                start = new TimeSpan(14, 0, 0);
                end = new TimeSpan(16, 0, 0);
                break;
            case DayOfWeek.Wednesday:
                start = new TimeSpan(8, 0, 0);
                end = new TimeSpan(12, 0, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(date), "Некорректный день недели (разрешены Пн/Вт/Ср).");
        }

        if (date.TimeOfDay < start || date.TimeOfDay >= end)
        {
            throw new ArgumentOutOfRangeException(
                nameof(date),
                $"Некорректное время. Для {date.DayOfWeek} доступно {start:hh\\:mm}–{end:hh\\:mm} (слот {WindowRangeMin} мин).");
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