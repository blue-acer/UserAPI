using System;
using System.Collections.Generic;

namespace UserAPI.Models
{
    public partial class UserDetails
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public byte StatusCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
