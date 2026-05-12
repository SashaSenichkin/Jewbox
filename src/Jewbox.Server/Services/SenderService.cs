using System.Text.RegularExpressions;
using Jewbox.Models;

namespace Jewbox.Services;

public partial class SenderService
{
    private readonly Uri GetUrl = new("https://probpalata.gov.ru/deyatelnost/kleymenie-i-markirovka/grafik-raboty-otdelov-po-klejmeniyu-i-markirovke/g-moskva-zapis-na-klejmenie-partij-po-srochnomu-tarifu-proizvoditeli");
    private readonly Uri PostUrl = new("https://probpalata.gov.ru/wp-admin/admin-ajax.php");
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SenderService> _logger;

    public SenderService(IHttpClientFactory httpClientFactory, ILogger<SenderService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public SentStatus SendRequest(Booking source)
    {
        var endDate = source.EndDate ?? source.DesiredDate.AddMinutes(15);
        var bookingCode = (int)source.Type;
        var client = _httpClientFactory.CreateClient();
        string secret;
        try
        {
            secret = GetSecret(CancellationToken.None);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return SentStatus.CantGetSecret;
        }

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = PostUrl,
            Headers =
            {
                { "Accept", "*/*" },
                { "Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                { "Connection", "keep-alive" },
                { "Origin", "https://probpalata.gov.ru" },
                { "Referer", "https://probpalata.gov.ru/" },
                { "Sec-Fetch-Dest", "empty" },
                { "Sec-Fetch-Mode", "cors" },
                { "Sec-Fetch-Site", "same-origin" },
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; Chromium GOST) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36" },
                { "X-Requested-With", "XMLHttpRequest" },
                { "sec-ch-ua", "\"Chromium\";v=\"146\", \"Not-A.Brand\";v=\"24\", \"Google Chrome\";v=\"146\"" },
                { "sec-ch-ua-mobile", "?0" },
                { "sec-ch-ua-platform", "\"Windows\"" },
            },
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "action", "WPBC_AJX_BOOKING__CREATE" },
                { "wpbc_ajx_user_id", "0" },
                { "nonce", secret },
                { "wpbc_ajx_locale", "ru_RU" },
                { "calendar_request_params[resource_id]", "108" },
                { "calendar_request_params[dates_ddmmyy_csv]", source.DesiredDate.ToString("dd.MM.yyyy") },
                {
                    "calendar_request_params[formdata]",
                    $"selectbox-one^rangetime{bookingCode}^{source.DesiredDate:HH:mm}-{endDate:HH:mm}~selectbox-one" +
                    $"^rangetime_val{bookingCode}^{source.DesiredDate:HH:mm}-{endDate:HH:mm}~text" +
                    $"^company_name{bookingCode}^{source.Person.OrgName}&#34;~text" +
                    $"^company_name_val{bookingCode}^{source.Person.OrgName}~text" +
                    $"^spec_uchet{bookingCode}^{source.Person.OrgCode}~text" +
                    $"^spec_uchet_val{bookingCode}^{source.Person.OrgCode}~text" +
                    $"^imennik{bookingCode}^{source.Person.NameCode}~text" +
                    $"^imennik_val{bookingCode}^{source.Person.NameCode}~text" +
                    $"^name{bookingCode}^{source.Person.Name}~text" +
                    $"^name_val{bookingCode}^{source.Person.Name}~email" +
                    $"^email{bookingCode}^{source.Person.Email}~email" +
                    $"^email_val{bookingCode}^{source.Person.Email}~text" +
                    $"^phone{bookingCode}^{source.Person.Phone}~text" +
                    $"^phone_val{bookingCode}^{source.Person.Phone}~text" +
                    $"^wpbc_other_action^~text" +
                    $"^wpbc_other_act_val{bookingCode}^"
                },
                { "calendar_request_params[booking_hash]", "" },
                { "calendar_request_params[custom_form]", source.Type.ToString() },
                { "calendar_request_params[aggregate_resource_id_arr]", "" },
                { "calendar_request_params[is_emails_send]", "1" },
                { "calendar_request_params[active_locale]", "ru_RU" },
            }),
        };

        try
        {
            using var response = client.Send(request);
            var body = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine("response" + DateTime.Now + body);

            if (string.IsNullOrWhiteSpace(body))
            {
                _logger.LogWarning("empty body {request}", request);
                return SentStatus.UnknownError;
            }

            if (!body.Contains("\"status\":\"error\""))
            {
                return SentStatus.Success;
            }

            _logger.LogInformation("DayBusy {request}", request);
            return SentStatus.DayBusy;
        }
        catch (Exception e)
        {
            _logger.LogError("exception got {request}, {errorMsg}", request, e.Message);
            return SentStatus.UnknownError;
        }
    }

    public string GetSecret(CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = GetUrl,
            Headers =
            {
                { "Accept", "*/*" },
                { "User-Agent", "Chrome/1.1.0" },
                { "Connection", "keep-alive" },
            },
        };
        using var response = client.Send(request, ct);
        var body = response.Content.ReadAsStringAsync(ct).Result;
        var regex = MyRegex();
        var match = regex.Matches(body).Single();
        var result = match.Value[match.Value.LastIndexOf('\'')..].Trim('\'');
        _logger.LogInformation("today's secret is: {result}", result);
        return result;
    }

    [GeneratedRegex(@"_wpbc.set_secure_param\(\s*'nonce',\s*'\w*")]
    private static partial Regex MyRegex();
}
