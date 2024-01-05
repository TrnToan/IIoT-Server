using MagicMirrorIotServer.Domain.Exceptions;

namespace MagicMirrorIotServer.Api.Application.Errors;

public class ErrorMessage
{
    public string ErrorCode { get; set; }
    public string Message { get; set; }

    public ErrorMessage(string errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }

    public static ErrorMessage FromException(Exception exception)
    {
        switch (exception)
        {
            case EntityNotFoundException:
                var entityNotFoundException = (EntityNotFoundException)exception;
                return new ErrorMessage("EntityNotFound", $"The {entityNotFoundException.EntityType.Name} with id {entityNotFoundException.EntityId} was not found.");
            case ChildEntityNotFoundException:
                var childEntityNotFoundException = (ChildEntityNotFoundException)exception;
                return new ErrorMessage("EntityNotFound", $"The {childEntityNotFoundException.ChildEntityType.Name} with id {childEntityNotFoundException.ChildEntityId} was not found in the {childEntityNotFoundException.ParentEntity.GetType().Name} with id {childEntityNotFoundException.ParentEntityId}.");
            case ChildEntityDuplicationException:
                var childEntityDuplicationException = (ChildEntityDuplicationException)exception;
                return new ErrorMessage("EntityDuplication", $"The {childEntityDuplicationException.ChildEntity.GetType().Name} with id {childEntityDuplicationException.ChildEntityId} already exists in the {childEntityDuplicationException.ParentEntity.GetType().Name} with id {childEntityDuplicationException.ParentEntityId}.");
            default:
                return new ErrorMessage("Unexpected", $"An unexpected error has occurred. {exception.Message}");
        }
    }
}
