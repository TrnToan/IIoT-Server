using MagicMirrorIotServer.Api.Infrastructure.Communication;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text.Json;

namespace MagicMirrorIotServer.Api.Hubs;

public class NotificationHub : Hub
{
    private readonly IEonNodeRepository _eonNodeRepository;
    private readonly SparkplugAdapter _sparkplugAdapter;
    private readonly TagValueCache _cache;

    public NotificationHub(IEonNodeRepository eonNodeRepository, SparkplugAdapter sparkplugAdapter, TagValueCache cache)
    {
        _eonNodeRepository = eonNodeRepository;
        _sparkplugAdapter = sparkplugAdapter;
        _cache = cache;
    }

    public async Task SendConfig(string eonNodeId)
    {
        var node = await _eonNodeRepository.GetNodeWithIdAsync(eonNodeId);
        if (node is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), eonNodeId);
        }

        foreach (var device in node.Devices)
        {
            await _sparkplugAdapter.PublishMetricCommands(node, device);
        }
    }

    public async Task SendTagsValue()
    {
        var tags = _cache.GetAllTags();
        string jsonTags = JsonConvert.SerializeObject(tags);
        await Clients.All.SendAsync("GetAll", jsonTags);
    }
}