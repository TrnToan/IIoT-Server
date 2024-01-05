using MagicMirrorIotServer.Api.Hubs;
using MagicMirrorIotServer.Api.Infrastructure.Caches;
using MagicMirrorIotServer.Domain.AggregateModels.EonNodeAggregate;
using Microsoft.AspNetCore.SignalR;
using ProtoBuf.WellKnownTypes;

namespace MagicMirrorIotServer.Api.Application.Events;

public class TagValueChangedEventHandler : INotificationHandler<TagValueChangedEvent>
{
    private readonly IEonNodeRepository _eonNodeRepository;
    private readonly TagValueCache _tagValueCache;
    private readonly IHubContext<NotificationHub> _hubContext;

    public TagValueChangedEventHandler(IEonNodeRepository eonNodeRepository, TagValueCache tagValueCache, IHubContext<NotificationHub> hubContext)
    {
        _eonNodeRepository = eonNodeRepository;
        _tagValueCache = tagValueCache;
        _hubContext = hubContext;
    }

    async Task INotificationHandler<TagValueChangedEvent>.Handle(TagValueChangedEvent notification, CancellationToken cancellationToken)
    {
        UpdateTagValueCache(notification.EonNodeId, notification.DeviceId, notification.TagId, notification.Value);

        await _hubContext.Clients.All.SendAsync("TagValueChanged", notification.EonNodeId, notification.DeviceId, 
            notification.TagId, notification.Value, notification.Timestamp, cancellationToken);       
    }

    private void UpdateTagValueCache(string eonNodeId, string deviceId, string tagId, object tagValue)
    {
        bool isTagInCache = _tagValueCache.CheckTagExistence(eonNodeId, deviceId, tagId);
        if (isTagInCache)
        {
            _tagValueCache.UpdateTag(eonNodeId, deviceId, tagId, tagValue);
        }
        else
        {
            _tagValueCache.AddTag(eonNodeId, deviceId, tagId, tagValue);
        }
    }
}