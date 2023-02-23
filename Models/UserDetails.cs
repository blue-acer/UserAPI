using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public partial class UserDetails
    {
        public long UserId { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string EmailAddress { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
