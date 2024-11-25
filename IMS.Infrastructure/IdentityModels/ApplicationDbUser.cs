using Microsoft.AspNetCore.Identity;

namespace IMS.Infrastructure.IdentityModels
{
    public class ApplicationDbUser : IdentityUser
    {
        public string? UserFName { get; set; }
        public string? UserLName { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsOneTimePass { get; set; } = false;
        public string? ProfileImageUrl { get; set; }


        public int UserTypeId { get; set; }
        public int UserStatusId { get; set; }
        public int? GenderId { get; set; }
        public int? SocialLoginTypeId { get; set; }
        public string? ReferByUserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? MyReferCode { get; set; }
        public decimal? CurrentBlance { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
