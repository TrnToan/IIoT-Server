namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class RemoveEonNodeCommandHandler : IRequestHandler<RemoveEonNodeCommand, bool>
{
    private readonly IEonNodeRepository _eonNodeRepository;

    public RemoveEonNodeCommandHandler(IEonNodeRepository eonNodeRepository)
    {
        _eonNodeRepository = eonNodeRepository;
    }

    public async Task<bool> Handle(RemoveEonNodeCommand request, CancellationToken cancellationToken)
    {
        var node = await _eonNodeRepository.GetNodeWithIdAsync(request.NodeId);
        if (node is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.NodeId);
        }

        _eonNodeRepository.RemoveNode(node);
        return await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
