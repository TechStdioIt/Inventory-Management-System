using IMS.Application.ServiceInterface;
using IMS.Domain.Models;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.IdentityModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Web;

namespace IMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministrator _administrator;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationDbUser> _userManager;
        private readonly SignInManager<ApplicationDbUser> _signInManager;

        public AdministratorController(IAdministrator administrator, RoleManager<IdentityRole> roleManager, UserManager<ApplicationDbUser> userManager, SignInManager<ApplicationDbUser> signInManager)
        {
            _administrator = administrator;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var data = await _administrator.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest("Error");
            }

        }

        [HttpGet("GetDropdownData")]
        public async Task<ActionResult> GetDropdownData(int flag)
        {
            var data = await _administrator.GetDropdownData(flag);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetAllActiveUsers")]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            try
            {
                var data = await _userManager.Users.Where(x => x.Status.Equals(true)).OrderByDescending(x => x.CreatedAt).ToListAsync();


                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("ListRoles")]
        public async Task<IActionResult> ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }
        [HttpPost("CreateRole")]

        public async Task<ActionResult> CreateRole([FromBody] AspNetRole createRoleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = createRoleViewModel.Name
                    };

                    // Saves the role in the underlying AspNetRoles table
                    var result = await _roleManager.CreateAsync(identityRole);

                    if (result.Succeeded)
                    {
                        return Ok(result);

                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

                return BadRequest("Role not Created");
            }
            catch (Exception ex)
            {
                return BadRequest("Role not Created");
            }
        }

        [HttpPut("EditRole/{id}")]
        public async Task<IActionResult> EditRole([FromBody] AspNetRole createRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(createRoleViewModel.Id);

            if (role == null)
            {
                return BadRequest($"Role with Id = {createRoleViewModel.Name} cannot be found");

            }
            else
            {
                role.Name = createRoleViewModel.Name;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest("Server Error");
            }
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return BadRequest($"Role with Id = {id} cannot be found");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest("Server Error");
            }
        }




        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleVM model)
        {
            try
            {
                // Validate the model
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid model");
                }

                // Ensure the user and role IDs are valid
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var roleExists = await _roleManager.RoleExistsAsync(model.RoleId);
                if (!roleExists)
                {
                    return NotFound("Role not found");
                }

                // Assign the role to the user
                var result = await _userManager.AddToRoleAsync(user, model.RoleId);

                // Check if the role assignment was successful
                if (result.Succeeded)
                {
                    return Ok("Role assigned successfully");
                }

                // Role assignment failed, return error messages
                return BadRequest(result.Errors.Select(e => e.Description));
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("FirstRegister")]
        public async Task<ActionResult<FirstRegisterVM>> Register(FirstRegisterVM model)
        {
            var identityUser = new ApplicationDbUser
            {
                Email = model.Email,
                UserName = model.Email,
                UserFName = model.UserFName,
                UserLName = model.UserLName,
                Mobile = model.Mobile

            };
            var result = await _userManager.CreateAsync(identityUser, model.Password);



            if (result.Succeeded)
            {


                IdentityRole identityRole = new IdentityRole
                {
                    Name = "Common"
                };

                // Saves the role in the underlying AspNetRoles table
                var roleResult = await _roleManager.CreateAsync(identityRole);

                if (roleResult.Succeeded)
                {

                    await _userManager.AddToRoleAsync(identityUser, "Common");


                    var userCount = await _userManager.Users.CountAsync(); // Get the number of users

                    // Check if the user count is 1
                    if (userCount == 1)
                    {
                        var menuData = await _administrator.SaveMenu();
                        //insert roleId and all menu Id from menuData
                        if (menuData != null)
                        {
                            foreach (var menuItem in menuData)
                            {
                                if (menuItem != null)
                                {
                                    var permissionData = await _administrator.MenuPermissionData(menuItem.Id, identityRole.Id);
                                }
                            }
                        }



                    }
                }

                return Ok(result);
            }
            return BadRequest();
        }

        //[HttpPost("CreateUser")]
        //public async Task<ActionResult<RegisterVM>> CreateUser([FromForm] IFormFile file, [FromForm] string json_data)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var data = JsonConvert.DeserializeObject<RegisterVM>(json_data);
        //		data.CreatedAt = data.UpdatedAt = DateTime.Now;
        //		if (await _administrator.CreateUserAsync(data, file))
        //		{
        //			return Ok();
        //		}
        //		else
        //		{
        //			return BadRequest();
        //		}
        //	}
        //	return BadRequest("Some properties are not valid");
        //}

        //[HttpPut("UpdateUser/{id}")]
        //public async Task<ActionResult<RegisterVM>> UpdateUser([FromForm] IFormFile file, [FromForm] string json_data)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var data = JsonConvert.DeserializeObject<RegisterVM>(json_data);
        //		var userWithId = await _userManager.FindByIdAsync(data.Id);

        //		if (userWithId != null)
        //		{
        //			var result = await _administrator.UpdateUserAsync(data, file);
        //			if (result != null)
        //			{
        //				return Ok(result);
        //			}
        //			return BadRequest("Not Successfully Updated");
        //		}
        //	}

        //	return BadRequest(" Not Successfully Updated");


        //}

        [HttpDelete("Delete/{ListOfId}")]
        public async Task<IActionResult> Delete(string ListOfId)
        {
            if (await _administrator.DeleteUsers(ListOfId))
            {
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] LoginVM loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // Get the user by email
                        var user = await _userManager.FindByEmailAsync(loginViewModel.UserName);
                        // Set the session role ID
                        HttpContext.Session.SetString("UserId", user.Id.ToString());

                        var roles = await _userManager.GetRolesAsync(user);
                        var roleIds = new List<string>();

                        foreach (var roleName in roles)
                        {
                            var role = await _roleManager.FindByNameAsync(roleName);
                            if (role != null)
                            {
                                roleIds.Add(role.Id);
                            }
                        }

                        // Check if the user is authenticated
                        bool isAuthenticated = User.Identity.IsAuthenticated;

                        var tokenResult = await _administrator.GenerateJwtToken(user.Id, string.Join(",", roles));

                        string Id = _userManager.GetUserId(HttpContext.User);
                        string? UserName = $"{user.UserFName} {user.UserLName}";

                        HttpContext.Session.SetString(Session.LoggedInUserId, Id.ToString());
                        HttpContext.Session.SetString(Session.LoggedInUserName, UserName);
                        HttpContext.Session.SetString(Session.LoggedInUserEmail, loginViewModel.UserName.ToString());
                        HttpContext.Session.SetString(Session.LoggedInUserTypeId, user.UserTypeId.ToString());


                        return Ok(new
                        {
                            result,
                            isAuthenticated,
                            user.Id,
                            user.UserFName,
                            user.UserLName,
                            user.UserTypeId,
                            user.UserStatusId,
                            tokenString = tokenResult.token,
                            refreshToken = tokenResult.refreshToken,
                            roleIds
                        });
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Informations");
                }
                return BadRequest("User not Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVM tokenRequest)
        {
            var principal = _administrator.GetPrincipalFromExpiredToken(tokenRequest.Token);
            if (principal == null) return BadRequest("Invalid token");

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var savedRefreshToken = _administrator.GetSavedRefreshToken(tokenRequest.RefreshToken);



            //var savedRefreshToken = "await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken)";

            if (savedRefreshToken == null || savedRefreshToken.Result.UserId != userId || savedRefreshToken.Result.ExpiryDate <= DateTime.UtcNow || savedRefreshToken.Result.IsRevoked)
            {
                return BadRequest("Invalid refresh token");
            }

            var roles = string.Join(",", principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
            var (newToken, newRefreshToken) = await _administrator.GenerateJwtToken(userId, roles);

            savedRefreshToken.Result.IsRevoked = true;

            //_context.RefreshTokens.Update(savedRefreshToken);
            //await _context.SaveChangesAsync();

            var updateRefreshToken = await _administrator.UpdateRefreshToken(savedRefreshToken.Result);

            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });

            //return Ok();
        }




        [HttpPost("ResetPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ResetPassword(ResetPassVM data)
        {
            if (ModelState.IsValid)
            {
                var UserId = HttpContext.Session.GetString("LoggedInUserId");
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null)
                {
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, data.NewPassword);
                    var response = new
                    {
                        status = 200,
                        message = "Success",
                        data = passwordChangeResult
                    };
                    return Ok(response);
                }
                var InvalidResponse = new
                {
                    status = 500,
                    message = "Invalid Credentials"
                };
                return BadRequest(InvalidResponse);
            }
            var InvalidUserResponse = new
            {
                status = 500,
                message = "Invalid User"
            };
            return BadRequest(InvalidUserResponse);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPassVM data)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(data.Email);
                if (user != null)
                {
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedCode = HttpUtility.UrlEncode(resetToken);
                    IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, data.Password);

                    return Ok(passwordChangeResult);
                }
                return BadRequest("Invalid Credentials");
            }
            return BadRequest("Invalid Credentials");
        }

        [HttpPut("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(ChangePassVM credential)
        {
            try
            {
                string UserId = HttpContext.Session.GetString("LoggedInUserId");
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null) return BadRequest("Invalid User");
                var result = await _userManager.ChangePasswordAsync(user, credential.OldPassword, credential.NewPassword);
                var response = new
                {
                    status = 200,
                    message = "Success",
                    data = result
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = 500,
                    message = ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }
    }
}
