using Jewbox.Models;

namespace Jewbox.Services;

internal interface ISenderService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    Task<SentStatus> SendRequestAsync(Booking source);
}