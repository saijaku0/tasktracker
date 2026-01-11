using TaskTracker.Domain.UserDomain;

namespace TaskTracker.Domain.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByUserId(Guid userId, CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task AddUser(User user, CancellationToken cancellationToken);

        Task UpdateUser(User user, CancellationToken cancellationToken);

        Task DeleteUser(Guid userId, CancellationToken cancellationToken);

        Task SaveUser(User user, CancellationToken cancellationToken);
    }
}