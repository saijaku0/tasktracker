namespace TaskTracker.Domain
{
    public record TaskCompletedDomainEvent(Guid TaskId, Guid AssignedUserId, DateTime CompletedAt);
}