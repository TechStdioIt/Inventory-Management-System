using IMS.Application.ServiceInterface;
using IMS.Domain.Models;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Infrastructure.ServiceRepository
{
    public class RoleServices : BaseRepository<AspNetRole>,IRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateOrUpdateRole(AspNetRoleVM role)
        {
            try
            {
                if(role.Id != null)
                {
                    var exist = await _roleManager.FindByIdAsync(role.Id);
                    if (exist != null)
                    {
                        exist.Name = role.RoleName;
                       await _roleManager.UpdateAsync(exist);
                        return true;
                    }
                    return false;
                }
                else
                {
                    var newRole = new IdentityRole()
                    {
                        Name=role.RoleName
                    };
                    var res = await _roleManager.CreateAsync(newRole);
                    if (res.Succeeded)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> GetAllRoleAsync()
        {
            try
            {
                return await _context.AspNetRoles.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<dynamic> GetRoleByRoleId(string roleId)
        {
            try
            {
                return await _context.AspNetRoles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
