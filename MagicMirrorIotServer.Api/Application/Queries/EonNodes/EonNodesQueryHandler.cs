using MagicMirrorIotServer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class EonNodesQueryHandler : IRequestHandler<EonNodesQuery, IEnumerable<EonNodeViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EonNodesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EonNodeViewModel>> Handle(EonNodesQuery request, CancellationToken cancellationToken)
    {
        var eonNodes = await _context.EonNodes
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<EonNodeViewModel>>(eonNodes);
    }
}
