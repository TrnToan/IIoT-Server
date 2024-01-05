namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class TagViewModel
{
    public string TagId { get; private set; }
    public string TagName { get; private set; }
    public TagType TagType { get; private set; }
    public string TagValue { get; private set; }
    public DateTime Timestamp { get; private set; }

    public TagViewModel(string tagId, string tagName, TagType tagType, string tagValue, DateTime timestamp)
    {
        TagId = tagId;
        TagName = tagName;
        TagType = tagType;
        TagValue = tagValue;
        Timestamp = timestamp;
    }

    private TagViewModel()
    {    }
}
