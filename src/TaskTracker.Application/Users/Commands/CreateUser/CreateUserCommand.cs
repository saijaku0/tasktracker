using MediatR;

namespace TaskTracker.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    )
        : IRequest<Guid>;
}