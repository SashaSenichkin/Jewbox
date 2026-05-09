using System.Net.Http.Json;
using Jewbox.Models;

namespace Jewbox.Services;

public class ApiSenderService : ISenderService
{
    private readonly HttpClient _http;

    public ApiSenderService(HttpClient http)
    {
        _http = http;
    }

    public async Task<SentStatus> SendRequestAsync(Booking source)
    {
        using var response = await _http.PostAsJsonAsync("api/booking/send", source);
        if (!response.IsSuccessStatusCode)
            return SentStatus.UnknownError;

        return await response.Content.ReadFromJsonAsync<SentStatus?>() ?? SentStatus.UnknownError;
    }
}
