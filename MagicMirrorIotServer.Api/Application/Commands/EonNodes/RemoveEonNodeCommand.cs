namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class RemoveEonNodeCommand : IRequest<bool> 
{
    public string NodeId { get; set; }

    public RemoveEonNodeCommand(string nodeId)
    {
        NodeId = nodeId;
    }
}
