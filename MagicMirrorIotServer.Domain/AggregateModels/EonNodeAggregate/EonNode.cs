using System.Threading.Tasks.Sources;

namespace MagicMirrorIotServer.Domain.AggregateModels.EonNodeAggregate;
public class EonNode : Entity, IAggregateRoot
{
    public EonNode(string eonNodeId, string eonNodeName)
    {
        EonNodeId = eonNodeId;
        EonNodeName = eonNodeName;
        Devices = new List<Device>();
    }

    public string EonNodeId { get; private set; }
    public string EonNodeName { get; private set; }
    public List<Device> Devices { get; private set; }

    public void AddDevice(string deviceId, string deviceName, string prototypeId, string devicePrototype)
    {
        var device = new Device(deviceId, deviceName, prototypeId, devicePrototype);
        if (Devices.Exists(d => d.DeviceId == deviceId))
        {
            throw new ChildEntityDuplicationException(deviceId, device, EonNodeId, this);
        }

        Devices.Add(device);
    }

    public void AddTagToDevice(string deviceId, string tagId, string tagName, TagType tagType)
    {
        var device = GetDeviceWithId(deviceId);

        try
        {
            device.AddTag(tagId, tagName, DateTime.UtcNow.AddHours(7), "0", tagType);
        }
        catch (Exception ex)
        {
            throw new DomainException($"EonNode with id {EonNodeId} throw an exception. See inner exception for details.", ex);
        }
    }

    public void RemoveDevice(string deviceId)
    {
        var device = GetDeviceWithId(deviceId);
        Devices.Remove(device);
    }

    public void RemoveTagFromDevice(string deviceId, string tagId)
    {
        var device = GetDeviceWithId(deviceId);
        
        try
        {
            device.RemoveTag(tagId);
        }
        catch (Exception ex)
        {
            throw new DomainException($"EonNode with id {EonNodeId} throw an exception. See inner exception for details.", ex);
        }
    }

    public Device GetDeviceWithId(string deviceId)
    {
        var device = Devices.Find(d => d.DeviceId == deviceId);
        if (device is null)
        {
            throw new ChildEntityNotFoundException(deviceId, typeof(Device), EonNodeId, this);
        }

        return device;
    }
}
