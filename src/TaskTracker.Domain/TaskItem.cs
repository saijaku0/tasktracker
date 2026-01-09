namespace TaskTracker.Domain
{
    public enum TaskStatus
    {
        Todo,
        InProgress,
        Done
    }

    public class TaskItem
    {
        public Guid Id { get; init; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public TaskStatus Status { get; private set; }
        public Guid? AssignedUserId { get; private set; }

        private TaskItem() { }

        public TaskItem(
            string title,
            string description,
            TaskStatus status,
            Guid? assignedUserId)
        {
            if (string.IsNullOrEmpty(title))
                throw new DomainException(nameof(title));

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = status;
            AssignedUserId = assignedUserId;
        }

        public void Complete()
        {
            if (AssignedUserId is null)
                throw new DomainException("Cannot complete task without an assigned user.");

            if (Status == TaskStatus.Done)
                throw new DomainException("Cannot complete task has already been done.");

            Status = TaskStatus.Done;
        }

        public void AssignUser(Guid userId)
        {
            AssignedUserId = userId;

            if (Status == TaskStatus.Todo)
                Status = TaskStatus.InProgress;
        }
    }
}
