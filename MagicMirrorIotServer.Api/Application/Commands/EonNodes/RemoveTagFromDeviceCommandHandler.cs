namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class RemoveTagFromDeviceCommandHandler : IRequestHandler<RemoveTagFromDeviceCommand, bool>
{
    private readonly IEonNodeRepository _eonNodeRepository;

    public RemoveTagFromDeviceCommandHandler(IEonNodeRepository eonNodeRepository)
    {
        _eonNodeRepository = eonNodeRepository;
    }

    public async Task<bool> Handle(RemoveTagFromDeviceCommand request, CancellationToken cancellationToken)
    {
        EonNode? eonNode = await _eonNodeRepository.GetNodeWithIdAsync(request.NodeId);
        if (eonNode is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.NodeId);
        }

        eonNode.RemoveTagFromDevice(request.DeviceId, request.TagId);
        _eonNodeRepository.UpdateNode(eonNode);
        return await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
