namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class AddDeviceToEonNodeCommandHandler : IRequestHandler<AddDeviceToEonNodeCommand, bool>
{
    private readonly IEonNodeRepository _eonNodeRepository;

    public AddDeviceToEonNodeCommandHandler(IEonNodeRepository eonNodeRepository)
    {
        _eonNodeRepository = eonNodeRepository;
    }

    public async Task<bool> Handle(AddDeviceToEonNodeCommand request, CancellationToken cancellationToken)
    {
        EonNode? eonNode = await _eonNodeRepository.GetNodeWithIdAsync(request.EonNodeId);
        if (eonNode is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.EonNodeId);
        }

        eonNode.AddDevice(request.DeviceId, request.DeviceName, request.ProtocolId, request.DeviceProtocol);
        return await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
