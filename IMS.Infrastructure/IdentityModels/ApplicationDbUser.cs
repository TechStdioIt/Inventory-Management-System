using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Infrastructure.IdentityModels
{
    public class ApplicationDbUser : IdentityUser
    {
        public string? UserFName { get; set; }
        public string? UserLName { get; set; }
        public string? Mobile { get; set; }
        public bool Status { get; set; } = true;
        public string? ProfileImageUrl { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserTypeId { get; set; }
        public int UserStatusId { get; set; }
        [NotMapped]
        public string Password { get; set; } = null!;
    }
}
