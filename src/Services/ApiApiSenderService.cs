using System.Net.Http.Json;
using Jewbox.Models;

namespace Jewbox.Services;

public class ApiApiSenderService : IApiSenderService
{
    private readonly HttpClient _http;

    public ApiApiSenderService(HttpClient http)
    {
        _http = http;
    }

    public SentStatus SendRequest(Booking source)
    {
        using var response = _http.PostAsJsonAsync("api/booking/send", source).Result;
        if (!response.IsSuccessStatusCode)
            return SentStatus.UnknownError;

        return response.Content.ReadFromJsonAsync<SentStatus?>().Result ?? SentStatus.UnknownError;
    }
}
