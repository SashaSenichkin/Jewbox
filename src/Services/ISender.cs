using Jewbox.Models;

namespace Jewbox.Services;

internal interface ISender
{
    Task<bool> SendRequest(Booking source);

    public Task<string> GetSecretAsync(CancellationToken ct);
}