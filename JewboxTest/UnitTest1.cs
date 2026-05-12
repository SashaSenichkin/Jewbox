using System.Net;
using System.Text.Json;
using Jewbox.Services;
using Moq;
using Moq.AutoMock;
using Moq.Protected;
using NUnit;


namespace JewboxTest;

public class Tests
{
    private AutoMocker _mocker;
    
    [SetUp]
    public void Setup()
    {
        _mocker = new AutoMocker();
    }

    [Test]
    public void Test1()
    {
        const string response = "'function' === typeof ( wpbc__conditions__SAVE_INITIAL__days_selection_params__bm ) ){ wpbc__conditions__SAVE_INITIAL__days_selection_params__bm( 108 ); }   wpbc_calendar_show( '108' );   _wpbc.set_secure_param( 'nonce',   '99483a3a70' );   _wpbc.set_secure_param( 'user_id', '0' );   _wpbc.set_secure_param( 'locale',  'ru_RU' );  wpbc_calendar__load_data__ajx( {\"resource_id\":108,\"booking_hash\":\"\",\"request_uri\":\"\\/deyatelnost\\/kleymenie-i-markirovka\\/grafik-raboty-otdelov-po-klejmeniyu-i-markirovke\\/g-moskva-zapis-na-klejmenie-partij-po-srochnomu-tarifu-proizvoditeli\\/\",\"custom_form\":\"proizvoditeli_sro4no\",\"aggregate_resource_id_str\":\"\",\"aggregate_type\":\"all\"} ); } ); }, 500 ); })();</script>";
        
        var responseObj = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(response),
        };

        var mockFactory = new Mock<IHttpClientFactory>(); 
        var httpMessageHandler = new Mock<HttpMessageHandler>();
        httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseObj);
        
        var httpClient = new HttpClient(httpMessageHandler.Object);

        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _mocker.Use(mockFactory);
        var sender = _mocker.Get<SenderService>();
        var result = sender.GetSecret(CancellationToken.None);

        Assert.That(result, Is.EqualTo("99483a3a70"));
    }
}