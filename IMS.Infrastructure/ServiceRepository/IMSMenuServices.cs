using System.Data;
using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.Models;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IMS.Infrastructure.ServiceRepository
{
    public class IMSMenuServices : BaseRepository<object>, IIMSMenu
    {
        public IMSMenuServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
        }
        public async Task<dynamic> CreateOrUpdateMenu(IMSMenuVm data)
        {
            try
            {
                var parameters = new DynamicParameters();

                if (data.id == 0)
                {
                    parameters.Add("@Flag", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@parentId", data.parentId);
                    parameters.Add("@title", data.title);
                    parameters.Add("@type", data.type);
                    parameters.Add("@url", data.url);
                    parameters.Add("@icon", data.icon);
                    parameters.Add("@target", data.target);
                    parameters.Add("@breadcrumbs", data.breadcrumbs);
                    parameters.Add("@classes",data.classes);


                }
                else
                {
                    parameters.Add("@Flag", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@parentId", data.parentId);
                    parameters.Add("@title", data.title);
                    parameters.Add("@type", data.type);
                    parameters.Add("@url", data.url);
                    parameters.Add("@icon", data.icon);
                    parameters.Add("@target", data.target);
                    parameters.Add("@breadcrumbs", data.breadcrumbs);
                    parameters.Add("@classes", data.classes);

                }
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SPMenu",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> DeleteMenu(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 4);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SPMenu",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<dynamic> GetAllMenu()
        {
            try
            {
                var menuItems = await _contextDapper.CreateConnection().QueryAsync<IMSMenu>("SELECT * FROM IMSMenu");

                var hierarchicalData = BuildMenuHierarchy(menuItems.ToList(), 0);
                return  hierarchicalData.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<dynamic> GetMenuById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 2);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SPMenu",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private  List<IMSMenu> BuildMenuHierarchy(List<IMSMenu> menuItems, int? parentId)
        {
            return menuItems
                .Where(m => m.ParentId == parentId)
                .Select(m => new IMSMenu
                {
                    Id = m.Id,
                    Title = m.Title,
                    Type = m.Type,
                    Url = m.Url,
                    Icon = m.Icon,
                    Target = m.Target,
                    Breadcrumbs = m.Breadcrumbs,
                    Classes = m.Classes,
                    Children = BuildMenuHierarchy(menuItems, m.Id)
                })
                .ToList();
        }
    }
}
