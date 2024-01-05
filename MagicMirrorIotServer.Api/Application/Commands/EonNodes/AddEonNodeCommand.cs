namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

[DataContract]
public class AddEonNodeCommand: IRequest<bool>
{
    [DataMember]
    public string EonNodeId { get; set; }
    [DataMember]
    public string EonNodeName { get; set; }

    public AddEonNodeCommand(string eonNodeId, string eonNodeName)
    {
        EonNodeId = eonNodeId;
        EonNodeName = eonNodeName;
    }
}
