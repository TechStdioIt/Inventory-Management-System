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
    public class BankServices : BaseRepository<object>, IBank
    {
        public BankServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
        }

        public async Task<dynamic> CreateOrUpdateBank(BankVM data)
        {
            try
            {
                var parameters = new DynamicParameters();

                if (data.id == 0)
                {

                    parameters.Add("@Flag", 1);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@name", data.name);
                    parameters.Add("@address",data.address);
                    parameters.Add("@branch",data.branch);
                    parameters.Add("@branch", data.branch);
                }
                else
                {
                    parameters.Add("@Flag", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@name", data.name);
                    parameters.Add("@address", data.address);
                    parameters.Add("@branch", data.branch);
                    parameters.Add("@branch", data.branch);
                }
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "Sp_Bank",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> GetAllBank()
        {
            try
            {
                
                    var parameters = new DynamicParameters();
                    parameters.Add("@Flag", 3);
                    using (var _dp = _contextDapper.CreateConnection())
                    {
                        var query = await _dp.QueryAsync<dynamic>(
                            "Sp_Bank",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );
                        return query;
                    }
               

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> GetBankById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 2);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "Sp_Bank",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> DeleteBank(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 4);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "Sp_Bank",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
