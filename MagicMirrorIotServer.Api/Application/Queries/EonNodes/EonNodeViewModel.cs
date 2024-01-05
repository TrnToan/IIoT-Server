namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class EonNodeViewModel
{
    public string EonNodeId { get; private set; }
    public string EonNodeName { get; private set; }

    public EonNodeViewModel(string eonNodeId, string eonNodeName)
    {
        EonNodeId = eonNodeId;
        EonNodeName = eonNodeName;
    }

    public EonNodeViewModel()
    {

    }
}
