using MagicMirrorIotServer.Domain.AggregateModels.EonNodeAggregate;
using ProtoBuf.Meta;
using SparkplugNet.Core.Application;
using SparkplugNet.VersionB;
using SparkplugNet.VersionB.Data;
using System.Threading.Tasks;

namespace MagicMirrorIotServer.Api.Infrastructure.Communication;

public class SparkplugAdapter
{
    private SparkplugApplication? _sparkplugApplication;
    private SparkplugApplicationOptions? _applicationOptions;

    public SparkplugAdapter()
    {

    }

    public event AsyncEventHandler<TagValueChangedEvent>? DeviceDataReceived;

    public async Task Connect(List<Tag> tags)
    {
        var knownMetrics = new List<Metric>();
        
        foreach (var tag in tags)
        {
            switch (tag.TagType)
            {
                case TagType.Boolean:
                    knownMetrics.Add(new Metric(tag.TagId, DataType.Boolean, false));
                    break;
                case TagType.Float:
                    knownMetrics.Add(new Metric(tag.TagId, DataType.Float, 0.0));
                    break;
                case TagType.Integer:
                    knownMetrics.Add(new Metric(tag.TagId, DataType.Int32, 0));
                    break;
                default:
                    break;
            }
        }
        
        _sparkplugApplication = new SparkplugApplication(knownMetrics);
        _applicationOptions = new SparkplugApplicationOptions("52.231.118.126", 1883, "applicationS2", "user", "password", false, "scada1", TimeSpan.FromSeconds(30), true, null, null, new CancellationToken());
        _sparkplugApplication.DeviceDataReceivedAsync += OnDeviceDataReceived;
        await _sparkplugApplication.Start(_applicationOptions);
    }

    private async Task OnDeviceDataReceived(SparkplugApplicationBase<Metric>.DeviceDataEventArgs e)
    {
        if (e.Metric.Value is not null)
        {
            var nodeId = e.EdgeNodeIdentifier;
            var deviceId = e.DeviceIdentifier;
            var tagName = e.Metric.Name;
            var value = e.Metric.Value;
            var timestamp = DateTimeOffset.FromUnixTimeMilliseconds((long)e.Metric.Timestamp).DateTime;
            var deviceMetricReceivedEvent = new TagValueChangedEvent(nodeId, deviceId, tagName, value, timestamp);
            if (DeviceDataReceived is not null)
            {
                await DeviceDataReceived.Invoke(this, deviceMetricReceivedEvent);
            }
        }
    }

    public async Task PublishMetricCommands(EonNode node, Device device)
    {
        var knownMetrics = new List<Metric>();
        // Init connection
        var knownDeviceMetrics = CreateDeviceMetrics(device);
        var knownNodeMetrics = CreateNodeMetrics(node);
        knownMetrics.AddRange(knownDeviceMetrics);
        knownMetrics.AddRange(knownNodeMetrics);
        _applicationOptions = new SparkplugApplicationOptions("52.231.118.126", 1883, "applicationP2", "user", "password", false, "scada1", TimeSpan.FromSeconds(30), true, null, null, new CancellationToken());
        _sparkplugApplication = new SparkplugApplication(knownMetrics);
        // Connect and Publish
        await _sparkplugApplication.Start(_applicationOptions);
        await _sparkplugApplication.PublishDeviceCommand(knownDeviceMetrics, "group1", node.EonNodeId, device.DeviceId);
        await _sparkplugApplication.PublishNodeCommand(knownNodeMetrics, "group1", node.EonNodeId);
    }

    private IEnumerable<Metric> CreateDeviceMetrics(Device device)
    {
        var knownMetrics = new List<Metric>();

        var firstMetric = new Metric("deviceId", DataType.String, device.DeviceId, DateTime.UtcNow.AddHours(7));
        var secondMetric = new Metric("deviceProtocol", DataType.String, device.DeviceProtocol, DateTime.UtcNow.AddHours(7));

        knownMetrics.Add(firstMetric);
        knownMetrics.Add(secondMetric);
        foreach (var tag in device.Tags)
        {
            object metricDataType = DataType.Boolean;
            int metricValue = 0;
            switch (tag.TagType)
            {
                case TagType.Boolean:
                    metricDataType = DataType.Boolean;
                    metricValue = 0;
                    break;
                case TagType.Integer:
                    metricDataType = DataType.Int32;
                    metricValue = 1;
                    break;
                case TagType.Float:
                    metricDataType = DataType.Float;
                    metricValue = 2;
                    break;
                default:
                    break;
            }
            knownMetrics.Add(new Metric(tag.TagId, (DataType)metricDataType, metricValue, DateTime.UtcNow.AddHours(7)));
        }

        return knownMetrics;
    }

    private IEnumerable<Metric> CreateNodeMetrics(EonNode node)
    {
        var knownMetrics = new List<Metric>();

        var firstMetric = new Metric("nodeId", DataType.String, node.EonNodeId, DateTime.UtcNow.AddHours(7));
        var secondMetric = new Metric("nodeName", DataType.String, node.EonNodeName, DateTime.UtcNow.AddHours(7));
        var firstCommand = new Metric("Node Control/Rebirth", DataType.String, "false/true tuỳ nút nhấn Rebirth trên app (nút này không được bấm tuỳ tiện, phải có warning)", DateTime.UtcNow.AddHours(7));
        var secondCommand = new Metric("Node Control/Reboot", DataType.String, "false/true tuỳ nút nhấn Reboot trên app (nút này không được bấm tuỳ tiện, phải có warning)", DateTime.UtcNow.AddHours(7));

        knownMetrics.Add(firstMetric); 
        knownMetrics.Add(secondMetric);
        knownMetrics.Add(firstCommand);
        knownMetrics.Add(secondCommand);
        foreach(var device in node.Devices)
        {
            var metric = new Metric(device.DeviceName, DataType.String, device.DeviceId, DateTime.UtcNow.AddHours(7));
            knownMetrics.Add(metric);
        }
        return knownMetrics;
    }
}