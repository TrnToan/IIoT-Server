using MagicMirrorIotServer.Api.Infrastructure.Communication;

namespace MagicMirrorIotServer.Api.Application.Host;

public class IotServerHost: BackgroundService
{
    private readonly SparkplugAdapter _sparkplugAdapter;
    private readonly IServiceScopeFactory _scopeFactory;

    public IotServerHost(SparkplugAdapter sparkplugAdapter, IServiceScopeFactory scopeFactory)
    {
        _sparkplugAdapter = sparkplugAdapter;
        _scopeFactory = scopeFactory;

        _sparkplugAdapter.DeviceDataReceived += OnDeviceDataReceived;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _scopeFactory.CreateScope();
        var nodeRepository = scope.ServiceProvider.GetRequiredService<IEonNodeRepository>();
        var eonNodes = await nodeRepository.GetAllAsync();
        var tags = eonNodes
            .SelectMany(n => n.Devices)
            .SelectMany(d => d.Tags)
            .ToList();

        await _sparkplugAdapter.Connect(tags);
    }

    private async Task OnDeviceDataReceived(object? sender, TagValueChangedEvent e)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var notification = new TagValueChangedEvent(e.EonNodeId, e.DeviceId, e.TagId, e.Value, e.Timestamp);
        await mediator.Publish(notification);
    }
}