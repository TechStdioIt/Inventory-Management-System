using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.ViewModels
{
        public class AspNetRoleVM
        {
        
            [Required]
            public string RoleName { get; set; }
        }
        public class AssignRoleVM
        {
            public string UserId { get; set; }
            public string RoleId { get; set; }
        }

        public class FirstRegisterVM
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? UserFName { get; set; }
            public string? UserLName { get; set; }
            public string? Mobile { get; set; }

        }

        public class RegisterVM
        {
            public string Id { get; set; } = null!;
            public string? Email { get; set; }
            public string? Password { get; set; }
            public bool IsAuthenticated { get; set; }
            public string? UserFName { get; set; }
            public string? UserLName { get; set; }
            public string? Mobile { get; set; }
            public string? OTP { get; set; }
            public string? UserName { get; set; }
            public string? UserRoleId { get; set; }

            public string? CreatedBy { get; set; }

            public string? UpdatedBy { get; set; }

            public DateTime? CreatedAt { get; set; }

            public DateTime? UpdatedAt { get; set; }
        }
        public class ForgetPassVM
        {
            public string? Password { get; set; }
            public string? Email { get; set; }
        }
        public class LoginVM
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public bool RememberMe { get; set; }
        }
        public class ChangePassVM
        {
            public string? OldPassword { get; set; }
            public string? NewPassword { get; set; }
        }
        public class ResetPassVM
        {
            public string? NewPassword { get; set; }
        }
        public class RefreshTokenVM
        {
            public int Id { get; set; }
            public string Token { get; set; }
            public string UserId { get; set; }
            public DateTime ExpiryDate { get; set; }
            public bool IsRevoked { get; set; }
        }
        public class TokenRequestVM
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }

}
