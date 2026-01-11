namespace TaskTracker.Domain.TasksItem
{
    public record TaskCompletedDomainEvent(Guid TaskId, Guid AssignedUserId, DateTime CompletedAt);
}