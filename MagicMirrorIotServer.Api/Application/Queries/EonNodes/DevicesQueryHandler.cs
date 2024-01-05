using MagicMirrorIotServer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class DevicesQueryHandler : IRequestHandler<DevicesQuery, IEnumerable<DeviceViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DevicesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeviceViewModel>> Handle(DevicesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EonNodes.AsNoTracking();

        var node = await queryable.Include(q => q.Devices).FirstOrDefaultAsync(q => q.EonNodeId == request.NodeId);
        if (node is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.NodeId);
        }

        var devices = node.Devices.ToList();
        return _mapper.Map<IEnumerable<DeviceViewModel>>(devices);
    }
}
