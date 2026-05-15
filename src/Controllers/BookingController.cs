using Jewbox.Models;
using Jewbox.Repositories;
using Jewbox.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jewbox.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookingController(
    ISenderService senderService,
    IBookingService bookingService,
    IUserRepository  userRepository)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> BookTime(int personId, BookingType bookingType, DateTime time)
    {
        var booking = bookingService.GetBooking(personId, bookingType, time);
        var result = await senderService.SendRequestAsync(booking);
        if (result == SentStatus.Success)
        {
            return Ok();
        }
        
        return BadRequest(result == SentStatus.CantGetSecret);
    }
    
    [HttpGet("CheckDate")]
    public IActionResult CheckDate(DateTime time)
    {
        return Ok(bookingService.IsCorrectDate(time));
    }
    
    [HttpGet("GetCandidates")]
    public IActionResult GetCandidates()
    {
        return Ok(userRepository.GetUsers());
    }
}