namespace MagicMirrorIotServer.Api.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public Type EntityType { get; }
    public object EntityId { get; }

    public EntityNotFoundException(Type entityType, object entityId) : base($"Entity of type {entityType.Name} with id {entityId} was not found.")
    {
        EntityId = entityId;
        EntityType = entityType;
    }
}
