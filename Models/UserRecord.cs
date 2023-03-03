namespace UserAPI.Models
{
    public class UserRecord
    {
        public string? UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
