namespace MagicMirrorIotServer.Domain.AggregateModels.EonNodeAggregate;
public interface IEonNodeRepository: IRepository<EonNode>
{
    EonNode Add(EonNode node);
    Task<EonNode?> GetNodeWithIdAsync(string nodeId);
    Task<bool> CheckTagExistenceAsync(string nodeId, string deviceId, string tagId);
    Task<Tag> GetTagAsync(string nodeId, string deviceId, string tagId);
    Task<IEnumerable<EonNode>> GetAllAsync();
    void RemoveNode(EonNode node);
    void UpdateNode(EonNode node);
}
