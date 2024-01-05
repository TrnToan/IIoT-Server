using System.Reflection.Metadata.Ecma335;

namespace MagicMirrorIotServer.Api.Application.Events;

public class TagValueChangedEventHandlerSave : INotificationHandler<TagValueChangedEvent>
{
    private readonly IEonNodeRepository _eonNodeRepository;
    private readonly ITagReadingRepository _tagReadingRepository;

    public TagValueChangedEventHandlerSave(IEonNodeRepository eonNodeRepository, ITagReadingRepository tagReadingRepository)
    {
        _eonNodeRepository = eonNodeRepository;
        _tagReadingRepository = tagReadingRepository;
    }

    public async Task Handle(TagValueChangedEvent notification, CancellationToken cancellationToken)
    {
        var tag = await _eonNodeRepository.GetTagAsync(notification.EonNodeId, notification.DeviceId, notification.TagId);
        TagReading<object> tagReading;
        switch (tag.TagType)
        {
            case TagType.Boolean:
                var boolValue = Convert.ToBoolean(notification.Value);
                tagReading = new TagReading<object>(tag.Id, boolValue, notification.Timestamp);
                break;
            case TagType.Integer:
                var intValue = Convert.ToInt32(notification.Value);
                tagReading = new TagReading<object>(tag.Id, intValue, notification.Timestamp);
                break;
            case TagType.Float:
                var floatValue = Convert.ToDouble(notification.Value);
                tagReading = new TagReading<object>(tag.Id, floatValue, notification.Timestamp);
                break;
            default:
                tagReading = new TagReading<object>(tag.Id, notification.Value, notification.Timestamp);
                break;
        }

        bool isExistedTagReading = await _tagReadingRepository.IsTagExistedAsync(tagReading.TagId, tagReading.Timestamp);
        if (!isExistedTagReading)
            await _tagReadingRepository.AddAsync(tagReading);

        await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}