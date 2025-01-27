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
    public class BusinessService : BaseRepository<BusinessVM>, IBusiness
    {
        public BusinessService(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {

        }

        public async Task<dynamic> GetPackageData()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 1);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    // Fetch master information
                    var masterInfo = await _dp.QueryAsync<dynamic>("SP_Package", parameters, commandType: CommandType.StoredProcedure);

                    // Fetch details information
                    parameters = new DynamicParameters(); // Reinitialize parameters to avoid duplication issues
                    parameters.Add("@Flag", 2);
                    var detailsInfo = await _dp.QueryAsync<dynamic>("SP_Package", parameters, commandType: CommandType.StoredProcedure);

                    // Combine masterInfo with detailsInfo by matching id
                    var combinedData = masterInfo.Select(master => new
                    {
                        id = master.id,
                        name = master.name,
                        detailsInfo = detailsInfo.Where(detail => detail.masterId == master.id).ToList()
                    }).ToList();

                    // Return the combined data directly
                    return combinedData;
                }
            }
            catch (Exception ex)
            {

                return ex;

            }
        }




    }
}
