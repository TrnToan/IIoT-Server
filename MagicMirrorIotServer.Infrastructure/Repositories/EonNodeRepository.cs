using System.Security.Cryptography.X509Certificates;

namespace MagicMirrorIotServer.Infrastructure.Repositories;
public class EonNodeRepository : BaseRepository, IEonNodeRepository
{
    public EonNodeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public EonNode Add(EonNode node)
    {
        if (node.IsTransient())
        {
            var entity = _context.EonNodes
                .Add(node)
                .Entity;

            return entity;
        }
        else
        {
            return node;
        }
    }

    public async Task<bool> CheckTagExistenceAsync(string nodeId, string deviceId, string tagId)
    {
        return await _context.EonNodes
            .AnyAsync(n => n.Devices
                .Any(d => d.Tags
                    .Any(t => t.TagId == tagId)));
    }

    public async Task<IEnumerable<EonNode>> GetAllAsync()
    {
        return await _context.EonNodes
            .Include(e => e.Devices)
            .ThenInclude(e => e.Tags)
            .ToListAsync();
    }

    public async Task<EonNode?> GetNodeWithIdAsync(string nodeId)
    {
        return await _context.EonNodes
            .Include(e => e.Devices)
            .ThenInclude(e => e.Tags)
            .FirstOrDefaultAsync(e => e.EonNodeId == nodeId);
    }

    public async Task<Tag> GetTagAsync(string nodeId, string deviceId, string tagId)
    {
        var eonNode = await _context.EonNodes
            .Include(eon => eon.Devices)
            .ThenInclude(d => d.Tags)
            .FirstOrDefaultAsync(x => x.EonNodeId == nodeId);
        var device = eonNode.Devices.Find(x => x.DeviceId == deviceId);
        return device.Tags.First(x => x.TagId == tagId);
    }

    public void RemoveNode(EonNode node)
    {
        _context.EonNodes.Remove(node);
    }

    public void UpdateNode(EonNode node)
    {
        _context.EonNodes.Update(node);
    }
}
