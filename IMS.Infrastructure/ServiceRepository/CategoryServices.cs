using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.ServiceRepository
{
    public class CategoryServices : BaseRepository<object>,ICategory
    {
        public CategoryServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
        }

        public async Task<dynamic> CreateOrUpdate(CategoryVM data)
        {
            try
            {
                var parameters = new DynamicParameters();

                if (data.id == 0)
                {

                    parameters.Add("@FLAG",1 );

                    parameters.Add("@userId", data.userId);
                    parameters.Add("@name", data.name);
                }
                else
                {
                    parameters.Add("@FLAG", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@name", data.name);
                }
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Category",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                } 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<dynamic> GetAllCategory()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 3);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<dynamic>(
                        "SP_Category",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<dynamic> GetCategoryById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 2);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Category",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
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
