using MagicMirrorIotServer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class TagQueryHandler : IRequestHandler<TagQuery, TagViewModel>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TagQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagViewModel> Handle(TagQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EonNodes.AsNoTracking();

        var node = await queryable
            .Include(q => q.Devices)
                .ThenInclude(q => q.Tags)
            .FirstOrDefaultAsync(q => q.EonNodeId == request.NodeId);
        if (node is null)
        {
            throw new EntityNotFoundException(typeof(EonNode), request.NodeId);
        }

        var device = node.Devices.Find(d => d.DeviceId == request.DeviceId);
        if (device is null)
        {
            throw new EntityNotFoundException(typeof(Device), request.DeviceId);
        }

        var tag = device.Tags.Find(t => t.TagId == request.TagId);
        if (tag is null)
        {
            throw new EntityNotFoundException(typeof(Tag), request.TagId);
        }

        return _mapper.Map<TagViewModel>(tag);
    }
}
