namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class AddEonNodeCommandHandler : IRequestHandler<AddEonNodeCommand, bool>
{
    private readonly IEonNodeRepository _eonNodeRepository;

    public AddEonNodeCommandHandler(IEonNodeRepository eonNodeRepository)
    {
        _eonNodeRepository = eonNodeRepository;
    }

    public async Task<bool> Handle(AddEonNodeCommand request, CancellationToken cancellationToken)
    {
        var eonNode = new EonNode(request.EonNodeId, request.EonNodeName);

        _eonNodeRepository.Add(eonNode);
        return await _eonNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
