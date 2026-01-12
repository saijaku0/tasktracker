using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Interface.Repository;
using TaskTracker.Domain.TasksItem;

namespace TaskTracker.Infrastructure.Persistence.Repository
{
    public class TaskItemRepository(ApplicationDbContext context)
        : ITaskRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.FindAsync<TaskItem>([id], cancellationToken);
        }

        public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(task, cancellationToken);
        }

        public Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            _context.Update(task);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<TaskItem>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<TaskItem>()
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}