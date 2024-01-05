using System.Runtime.CompilerServices;

namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

[DataContract]
public class CreateTagViewModel
{
    [DataMember]
    public string TagId { get; set; }
    [DataMember]
    public string TagName { get; set; }
    [DataMember]
    public TagType TagType { get; set; }

    public CreateTagViewModel(string tagId, string tagName, TagType tagType)
    {
        TagId = tagId;
        TagName = tagName;
        TagType = tagType;
    }
}
