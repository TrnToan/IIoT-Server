namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class AddTagToDeviceCommandHandler : IRequestHandler<AddTagToDeviceCommand, bool>
{
    private readonly IEonNodeRepository _eonNodeRepository;

    public AddTagToDeviceCommandHandler(IEonNodeRepository eonNodeRepository)
    {
        _eonNodeRepository = eonNodeRepository;
    }

    public async Task<bool> Handle(AddTagToDeviceCommand request, CancellationToken cancellationToken)
    {
        EonNode? eonNode = await _eonNodeRepository.GetNodeWithIdAsync(request.EonNodeId);
        if (eonNode is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.EonNodeId);
        }

        eonNode.AddTagToDevice(request.DeviceId, request.TagId, request.TagName, request.TagType);

        return await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
