namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class AddTagToDeviceCommand: IRequest<bool>
{
    public string EonNodeId { get; set; }
    public string DeviceId { get; set; }
    public string TagId { get; set; }
    public string TagName { get; set; }
    public TagType TagType { get; set; }

    public AddTagToDeviceCommand(string eonNodeId, string deviceId, string tagId, string tagName, TagType tagType)
    {
        EonNodeId = eonNodeId;
        DeviceId = deviceId;
        TagId = tagId;
        TagName = tagName;
        TagType = tagType;
    }
}
