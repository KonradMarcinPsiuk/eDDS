namespace ZoneTriggerService;

public class QualityService
{
    private HttpClient _client;

    public QualityService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(ClientNames.Names.ZoneTrigger);

    }
}