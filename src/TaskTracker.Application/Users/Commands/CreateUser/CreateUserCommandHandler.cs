using MediatR;
using TaskTracker.Application.Common.Interfaces.Authentication;
using TaskTracker.Domain.Interface.Repository;
using TaskTracker.Domain.UserDomain;

namespace TaskTracker.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
        : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Guid> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var emailExists = await _userRepository
                .GetByEmailAsync(request.Email, cancellationToken);

            if (emailExists is not null)
                throw new Exception("Email already in use.");

            if (request.Password.Length < 6)
                throw new Exception("Password must be at least 6 characters long.");

            string password = _passwordHasher.HashPassword(request.Password);

            var user = new User
            (
                request.FirstName,
                request.LastName,
                request.Email,
                password
            );

            await _userRepository.AddUser(user, cancellationToken);
            await _userRepository.SaveUser(cancellationToken);

            return user.Id;
        }
    }
}