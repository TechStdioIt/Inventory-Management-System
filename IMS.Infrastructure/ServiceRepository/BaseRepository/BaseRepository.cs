using IMS.Application.ServiceInterface;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.ServiceRepository.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseInterface<TEntity> where TEntity : class
    {
        protected readonly IMSContextEF _context;
        protected readonly ApplicationDbContext _applicationDb;
        protected readonly IMSContextDapper _contextDapper;
        protected UserManager<ApplicationDbUser> _userManager;
        protected readonly IConfiguration _configuration;


        public BaseRepository(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _applicationDb = applicationDb;
            _contextDapper = contextDapper;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<TEntity> FindByIdAsync(int id)
        {
            try
            {
                return _context.Set<TEntity>().Find(id);
            }
            catch (Exception err)
            { 
                Console.WriteLine(err.Message);
                throw err;
            }
        }

        public async Task<TEntity> FindByNoAsync(string no)
        {
            try
            {
                return _context.Set<TEntity>().Find(no);
            }
            catch (Exception err)
            {

               Console.WriteLine(err.Message)
            ;  throw err;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                var data = await _context.Set<TEntity>().ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                var result = await SaveChange() > 0;
                return result;
            }
            catch (Exception err)
            {

                Console.Write(err.Message);
                return false;
            }
        }

        public async Task<bool> updateByAsync(TEntity entity)
        {
            try
            {
                var result = _context.Set<TEntity>().Attach(entity);
                result.State = EntityState.Modified;
                var isExecuted = await SaveChange() > 0;
                return isExecuted;
            }
            catch (Exception err)
            {

                Console.Write(err.Message);
                return false;
            }
        }
        public async Task<int> SaveChange()
        {
            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}