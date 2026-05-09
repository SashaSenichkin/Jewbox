using Jewbox.Models;

namespace Jewbox.Services;

public interface ISenderService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    Task<SentStatus> SendRequestAsync(Booking source);
}