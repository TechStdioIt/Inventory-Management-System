using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.Models;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Infrastructure.ServiceRepository
{
    public class IMSMenuServices : BaseRepository<IMSMenu>, IIMSMenu
    {
        public IMSMenuServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
        }

        public async Task<IEnumerable<IMSMenu>> GetMenuAsync()
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
