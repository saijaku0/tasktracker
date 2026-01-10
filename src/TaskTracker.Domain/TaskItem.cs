namespace TaskTracker.Domain
{
    public enum TaskStatus
    {
        Todo,
        Cancelled,
        InProgress,
        Done
    }

    public class TaskItem
    {
        private readonly List<object> _domainEvents = [];

        public Guid Id { get; init; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string? CancellationReason { get; private set; } = null;
        public TaskStatus Status { get; private set; }
        public Guid? AssignedUserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private TaskItem() { }

        public TaskItem(
            string title,
            string description)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException(nameof(title));
            if (string.IsNullOrEmpty(description))
                throw new DomainException(nameof(description));

            DateTime now = DateTime.UtcNow;

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = TaskStatus.Todo;
            CreatedAt = now;
            UpdateAt = now;
        }

        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        public void Complete()
        {
            CheckAssignedUser();

            if (Status == TaskStatus.Done)
                throw new DomainException("Cannot complete task has already been done.");

            Status = TaskStatus.Done;
            UpdateAt = DateTime.UtcNow;

            var @event = new TaskCompletedDomainEvent(
                Id, AssignedUserId: AssignedUserId.Value, CompletedAt: UpdateAt);
            _domainEvents.Add(@event);
        }

        public void StartTaskProgress()
        {
            CheckAssignedUser();

            if (Status == TaskStatus.Done)
                throw new DomainException("Cannot start a completed task.");

            Status = TaskStatus.InProgress;
            UpdateAt = DateTime.UtcNow;
        }

        public void Cancel(string reason)
        {
            if (Status == TaskStatus.Done)
                throw new DomainException("Cannot cancel task has already been done.");

            if (Status == TaskStatus.Cancelled)
                throw new DomainException("Task has already been cancelled.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new DomainException("A cancellation reason is required.");

            Status = TaskStatus.Cancelled;
            UpdateAt = DateTime.UtcNow;
            CancellationReason = reason;
        }

        public void AssignUser(Guid userId)
        {
            if (Status == TaskStatus.Done)
                throw new DomainException("You cannot assign new user to a task that has been completed.");

            AssignedUserId = userId;

            if (Status == TaskStatus.Todo)
            {
                StartTaskProgress();
            }

            UpdateAt = DateTime.UtcNow;
        }

        public void Unassign()
        {
            if (Status == TaskStatus.Done)
                throw new DomainException("You cannot unassign person if task has already been done.");

            AssignedUserId = null;
            UpdateAt = DateTime.UtcNow;

            if (Status == TaskStatus.InProgress)
            {
                Status = TaskStatus.Todo;
            }
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        private void CheckAssignedUser()
        {
            if (AssignedUserId is null)
                throw new DomainException("Cannot complete task without an assigned user.");
        }
    }
}