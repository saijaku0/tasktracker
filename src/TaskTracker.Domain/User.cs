namespace TaskTracker.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string Lastname { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private User() { }

        public User(
            Guid id,
            string firstName,
            string lastname,
            string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastname))
                throw new DomainException("Last name is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is required.");

            Id = id;
            FirstName = firstName;
            Lastname = lastname;
            Email = email;
        }
    }
}