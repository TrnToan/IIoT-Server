namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class RemoveDeviceFromNodeCommandHandler : IRequestHandler<RemoveDeviceFromNodeCommand, bool>
{
    private readonly IEonNodeRepository _eonNodeRepository;

    public RemoveDeviceFromNodeCommandHandler(IEonNodeRepository eonNodeRepository)
    {
        _eonNodeRepository = eonNodeRepository;
    }

    public async Task<bool> Handle(RemoveDeviceFromNodeCommand request, CancellationToken cancellationToken)
    {
        var node = await _eonNodeRepository.GetNodeWithIdAsync(request.NodeId);
        if (node is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.NodeId);
        }

        node.RemoveDevice(request.DeviceId);
        _eonNodeRepository.UpdateNode(node);
        return await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
