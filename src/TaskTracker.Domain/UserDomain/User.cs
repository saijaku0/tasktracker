using TaskTracker.Domain.Exceptions;

namespace TaskTracker.Domain.UserDomain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string Lastname { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public Role Role { get; private set; }

        private User() { }

        public User(
            string firstName,
            string lastname,
            string email,
            string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastname))
                throw new DomainException("Last name is required.");
            //TODO : Add proper email validation
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is required.");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password is required.");

            Id = Guid.NewGuid();
            FirstName = firstName;
            Lastname = lastname;
            Email = email;
            PasswordHash = passwordHash;
        }

        public override string ToString()
        {
            return $"{FirstName} {Lastname} ({Email}) - Role: {Role}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not User other)
                return false;
            return Id == other.Id &&
                   FirstName == other.FirstName &&
                   Lastname == other.Lastname &&
                   Email == other.Email &&
                   PasswordHash == other.PasswordHash &&
                   Role == other.Role;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, Lastname, Email, PasswordHash, Role);
        }

        public void AssignRole(Role role)
        {
            if (!Enum.IsDefined(role))
                throw new DomainException("Invalid role assigned to user.");

            Role = role;
        }

        public void AssignEmail(string email)
        {
            //TODO : Add proper email validation
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is required.");

            Email = email;
        }

        public void AssignPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("Password is required.");

            PasswordHash = password;
        }

        public void AssignFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required.");
            FirstName = firstName;
        }

        public void AssignLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name is required.");
            Lastname = lastName;
        }

        public void PasswordReset(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new DomainException("New password is required.");
            PasswordHash = newPassword;
        }
    }
}