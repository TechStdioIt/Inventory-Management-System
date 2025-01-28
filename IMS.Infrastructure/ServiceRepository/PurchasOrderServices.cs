using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.ServiceRepository
{
    public class PurchasOrderServices : BaseRepository<PurchasOrderVM>, IPurchasOrder
    {
        public PurchasOrderServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {

        }

        public async Task<dynamic> CreateOrUpdatePurchaseOrder (string data)
        {
            try
             {

                var parameters = new DynamicParameters();

                parameters.Add("@Flag", 1);
                parameters.Add("@PurchaseOrderJSON", data);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SPPurchaseOrderCRUD",
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
        public async Task<dynamic> GetAllpurchaseOrder()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", 2);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    // Fetch master information
                    var masterInfo = await _dp.QueryAsync<dynamic>("SPPurchaseOrderCRUD", parameters, commandType: CommandType.StoredProcedure);

                    // Fetch details information
                    parameters = new DynamicParameters(); // Reinitialize parameters to avoid duplication issues
                    parameters.Add("@Flag", 3);
                    var detailsInfo = await _dp.QueryAsync<dynamic>("SPPurchaseOrderCRUD", parameters, commandType: CommandType.StoredProcedure);

                    // Combine masterInfo with detailsInfo by matching id
                    var combinedData = masterInfo.Select(master => new
                    {
                        id = master.id,
                        orderDate = master.orderDate,
                        totalAmount = master.totalAmount,
                        shippingCost = master.shippingCost,
                        supplierId = master.supplierId,
                        supplierName = master.supplierName,
                        detailsInfo = detailsInfo.Where(detail => detail.purchaseOrderId == master.id).ToList()
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

    // Define a model for ProductList
    public class ProductJSON
    {
        [JsonProperty("detailId")]
        public int detailId { get; set; }

        [JsonProperty("purchaseOrderId")]
        public int? purchaseOrderId { get; set; }

        [JsonProperty("productId")]
        public int? productId { get; set; }

        [JsonProperty("quantity")]
        public decimal? quantity { get; set; }

        [JsonProperty("unitPrice")]
        public decimal? unitPrice { get; set; }

        [JsonProperty("totalPrice")]
        public decimal? totalPrice { get; set; }
    }
}
