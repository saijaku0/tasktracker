using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Interface.Repository;
using TaskTracker.Domain.UserDomain;

namespace TaskTracker.Infrastructure.Persistence.Repository
{
    public class UserRepository(ApplicationDbContext context)
        : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User?> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync([userId], cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(email);

            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == email, cancellationToken);
        }

        public async Task AddUser(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public Task UpdateUser(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var user = await GetByUserId(userId, cancellationToken);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public async Task SaveUser(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}