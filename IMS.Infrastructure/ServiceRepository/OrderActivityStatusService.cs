using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace IMS.Infrastructure.ServiceRepository
{
    public class OrderActivityStatusService : BaseRepository<object>, IOrderActivityStatus
    {
        public OrderActivityStatusService(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {

        }
        public async Task<dynamic> CreateOrUpdateOrderActivityStatus(OrderActivityStatusVM data)
        {
            try
            {
                var parameters = new DynamicParameters();

                if (data.id == 0)
                {
                    parameters.Add("@Flag", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@name",data.name);
                }
                else
                {
                    parameters.Add("@Flag", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@name", data.name);
                }
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SPOrderActivityStatus",
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

        public async Task<dynamic> DeleteOrderActivityStatus(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 4);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SPOrderActivityStatus",
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

        public async Task<dynamic> GetAllOrderActivityStatus()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 3);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<dynamic>(
                        "SPOrderActivityStatus",
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

        public async Task<dynamic> GetOrderActivityStatusById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 2);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "GetAllOrderActivityStatus",
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
    }
}
