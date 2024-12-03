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
    public class SuppliersServices : BaseRepository<object>,ISuppliers
    {
        public SuppliersServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
        }
        public async Task<dynamic> CreateOrUpdate(SuppliersVm data)
        {
            try
            {
                var parameters = new DynamicParameters();

                if (data.id == 0)
                 {

                    parameters.Add("@Flag", 1);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@companyName", data.companyName);
                    parameters.Add("@contactName", data.contactName);
                    parameters.Add("@contactTitle", data.contactTitle);
                    parameters.Add("@street", data.street);
                    parameters.Add("@city", data.city);
                    parameters.Add("@province", data.province);
                    parameters.Add("@postalCode", data.postalCode);
                    parameters.Add("@country", data.country);
                    parameters.Add("@phone", data.phone);
                    parameters.Add("@email", data.email);
                }
                else
                {
                    parameters.Add("@Flag", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@companyName", data.companyName);
                    parameters.Add("@contactName", data.contactName);
                    parameters.Add("@contactTitle", data.contactTitle);
                    parameters.Add("@street", data.street);
                    parameters.Add("@city", data.city);
                    parameters.Add("@province", data.province);
                    parameters.Add("@postalCode", data.postalCode);
                    parameters.Add("@country", data.country);
                    parameters.Add("@phone", data.phone);
                    parameters.Add("@email", data.email);
                }
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Suppliers",
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
        public async Task<dynamic> GetSuppliersById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 2);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Suppliers",
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

        public async Task<dynamic> GetAllSuppliers()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 3);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<dynamic>(
                        "SP_Suppliers",
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


        public async Task<dynamic> DeleteSuppliers(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 4);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Suppliers",
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
