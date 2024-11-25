using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IMS.Infrastructure.ServiceRepository
{
    public class AdministratorServices : BaseRepository<object>, IAdministrator
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdministratorServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration,RoleManager<IdentityRole> roleManager) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> CreateUserAsync(RegisterVM model, IFormFile file)
        {
            try
            {
                string uniqueFileNameProfileImage = UploadProfileImage(file);
                if (uniqueFileNameProfileImage == null)
                {
                    uniqueFileNameProfileImage = "default.jpg";
                }
                var identityUser = new ApplicationDbUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Mobile = model.Mobile,
                    CreatedBy = model.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UserFName = model.UserFName,
                    UserLName = model.UserLName,
                    ProfileImageUrl = uniqueFileNameProfileImage,

                };
                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (result.Succeeded)
                {
                    string[] RoleIdList = model.UserRoleId.Split(',');

                    foreach (var item in RoleIdList)
                    {
                        _applicationDb.UserRoles.Add(new IdentityUserRole<string> { UserId = identityUser.Id, RoleId = item });
                        await _applicationDb.SaveChangesAsync();
                    }
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<bool> UpdateUserAsync(RegisterVM model, IFormFile file)
        {
            try
            {
                if (model == null)
                    throw new NullReferenceException("Register Model is null");
                var user = await _userManager.FindByIdAsync(model.Id);


                if (!file.FileName.ToLower().Contains("previous"))
                {
                    string uploadsFolder = _configuration.GetSection("UploadFilesPath").Value;
                    string filePath = Path.Combine(uploadsFolder, user.ProfileImageUrl ?? "e6ca9ef4-d103-4f67-a34c-77e9ab33a85d_file.png");
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    string uniqueFileNameProfileImage = UploadProfileImage(file);
                    if (uniqueFileNameProfileImage == null)
                    {
                        uniqueFileNameProfileImage = "default.jpg";
                    }
                    user.ProfileImageUrl = uniqueFileNameProfileImage;
                }
                user.Mobile = model.Mobile;
                user.UpdatedBy = model.UpdatedBy;
                user.UpdatedAt = DateTime.Now;
                user.UserFName = model.UserFName;
                user.UserLName = model.UserLName;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    //decare RoleIdList
                    if (model.UserRoleId != null)
                    {

                        string[] RoleIdList = model.UserRoleId.Split(',');
                        var existRecord = _applicationDb.UserRoles.Where(x => x.UserId.Equals(user.Id)).ToList();

                        var deletableRecord = existRecord
     .Where(x => !RoleIdList.Any(y => y.Trim().Equals(x.RoleId.Trim(), StringComparison.OrdinalIgnoreCase)))
     .ToList();

                        if (deletableRecord.Count > 0)
                        {
                            _applicationDb.UserRoles.RemoveRange(deletableRecord);
                            await _applicationDb.SaveChangesAsync();

                        }
                        var newRoles = RoleIdList
    .Where(x => !existRecord.Any(y => y.RoleId.Trim().Equals(x.Trim(), StringComparison.OrdinalIgnoreCase)))
    .ToList();
                        if (RoleIdList[0].Length > 0)
                        {

                            foreach (var item in newRoles)
                            {
                                _applicationDb.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = item });
                                await _applicationDb.SaveChangesAsync();
                            }
                        }
                    }

                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string UploadProfileImage(IFormFile file)
        {
            string uniqueFileNamePhoto = null;

            if (file != null || file.Length == 0)
            {
                string uploadsFolder = _configuration.GetSection("UploadFilesPath").Value;
                Directory.CreateDirectory(uploadsFolder);
                uniqueFileNamePhoto = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileNamePhoto);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileNamePhoto;
        }

        public async Task<bool> DeleteUsers(string listOfIds)
        {
            try
            {

                var parameters = new DynamicParameters();
                parameters.Add("@ListOfIds", listOfIds);
                parameters.Add("@Flag", 3);

                var result = _contextDapper.CreateConnection().Query<dynamic>("SP_UserManager", parameters, commandType: CommandType.StoredProcedure);

                // Check if there's any result returned
                if (result != null && result.Count() == 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }


        }

        public async Task<(string token, string refreshToken)> GenerateJwtToken(string userId, string roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
        new Claim(ClaimTypes.Name, userId.ToString()),
        new Claim(ClaimTypes.Role, string.Join(",", roles))
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var refreshToken = new RefreshTokenVM
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            // Save refresh token to database
            //_context.RefreshTokens.Add(refreshToken);
            //await _context.SaveChangesAsync();

            var parameters = new DynamicParameters();
            parameters.Add("@Flag", 1);
            parameters.Add("@Token", refreshToken.Token);
            parameters.Add("@ExpiryDate", refreshToken.ExpiryDate);
            parameters.Add("@IsRevoked", refreshToken.IsRevoked);
            parameters.Add("@UserId", refreshToken.UserId);

            using (var _dp = _contextDapper.CreateConnection())
            {
                var query = await _dp.QueryAsync<dynamic>("SP_UserManager", parameters, commandType: CommandType.StoredProcedure);

            }

            return (tokenString, refreshToken.Token);
        }

        ClaimsPrincipal IAdministrator.GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
            throw new SecurityTokenException("Invalid token");
        }

        public async Task<RefreshTokenVM> GetSavedRefreshToken(string token)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 2);
                parameters.Add("@Token", token);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<RefreshTokenVM>("SP_UserManager", parameters, commandType: CommandType.StoredProcedure);
                    return query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<dynamic> UpdateRefreshToken(RefreshTokenVM data)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 4);
                parameters.Add("@IsRevoked", data.IsRevoked);
                parameters.Add("@RefreshTokenId", data.Id);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<RefreshTokenVM>("SP_UserManager", parameters, commandType: CommandType.StoredProcedure);
                    return query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dynamic> SaveMenu()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 4);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<RefreshTokenVM>("SP_UserManager", parameters, commandType: CommandType.StoredProcedure);
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dynamic> MenuPermissionData(int menuId, string roleId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 5);
                parameters.Add("@MenuId", menuId);
                parameters.Add("@RoleId", roleId);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<RefreshTokenVM>("SP_UserManager", parameters, commandType: CommandType.StoredProcedure);
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dynamic> GetDropdownData(int flag)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", flag);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<dynamic>("DropDownSp", parameters, commandType: CommandType.StoredProcedure);
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
