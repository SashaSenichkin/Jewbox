using Jewbox.Models;

namespace Jewbox.Services;

public interface IApiSenderService
{
    SentStatus SendRequest(Booking source);
}