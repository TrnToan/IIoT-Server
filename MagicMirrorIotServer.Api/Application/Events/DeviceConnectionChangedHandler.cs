using MagicMirrorIotServer.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MagicMirrorIotServer.Api.Application.Events;

public class DeviceConnectionChangedHandler : INotificationHandler<DeviceConnectionChangedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public Task Handle(DeviceConnectionChangedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
