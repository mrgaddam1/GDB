using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class StaterRepository : IStaterRepository
    {
        private GDBContext DbContext { get; set; }
        private readonly ILogger<StaterRepository> logger;

        public StaterRepository(GDBContext _DbContext, ILogger<StaterRepository> logger)
        {
            DbContext = _DbContext;
            this.logger = logger;
        }
        public async Task<List<StatersViewModel>> GetAllStaters()
        {
            var statersData = new List<StatersViewModel>();
            statersData = await (from s in DbContext.Staters.AsNoTracking() join sp in DbContext.StaterPrices.AsNoTracking()
                                    on s.StaterId equals sp.StaterId 
                                 select new StatersViewModel
                                 {
                                     StaterId = s.StaterId,
                                     StarterDescription = s.StaterDescription,
                                     StaterPrice = sp.StaterPrice1,
                                 }).OrderBy(x => x.StarterDescription).ToListAsync();
            return statersData;
        }

        public async Task<decimal> GetStaterPriceByStater(int staterId)
        {
            return await (from s in DbContext.Staters.AsNoTracking()
                          join sp in DbContext.StaterPrices.AsNoTracking()
                          on s.StaterId equals sp.StaterId
                          where s.StaterId == staterId
                          select sp.StaterPrice1).SingleOrDefaultAsync();

        }

        public async Task<bool> Add(StatersViewModel statersViewModel)
        {
            try
            {
                var stater = new Stater
                {
                    UserId = 1,
                    StaterDescription = statersViewModel.StarterDescription,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };
                DbContext.Staters.Add(stater);
                await DbContext.SaveChangesAsync();

                var starterPrice = new StaterPrice
                {
                    StaterId = stater.StaterId,
                    StaterPrice1 = statersViewModel.StaterPrice??0,
                };
                DbContext.StaterPrices.Add(starterPrice);
                await DbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<bool> Update(StatersViewModel statersViewModel)
        {
            try
            {
                var existingStarterData = (DbContext.Staters.SingleOrDefault(x => x.StaterId == statersViewModel.StaterId));

                if (existingStarterData == null)
                {
                    logger.LogWarning("Stater with ID {LocationId} not found.", statersViewModel.StaterId);
                    return false;
                }
                if (existingStarterData != null)
                {
                    existingStarterData.StaterDescription = statersViewModel.StarterDescription;
                    existingStarterData.UserId = 1;
                    existingStarterData.ModifiedDate = DateTime.UtcNow;
                }
                DbContext.Staters.Update(existingStarterData);

                var existingStaterPrice = (DbContext.StaterPrices.SingleOrDefault(x => x.StaterId == statersViewModel.StaterId));
                if (existingStaterPrice == null)
                {
                    logger.LogWarning("Stater Price with ID {LocationId} not found.", statersViewModel.StaterId);
                    return false;
                }
                if (existingStaterPrice != null)
                {
                    existingStaterPrice.StaterPrice1 = statersViewModel.StaterPrice??0;
                    existingStaterPrice.StaterId = statersViewModel.StaterId;                  
                }
                DbContext.StaterPrices.Update(existingStaterPrice);

                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }







    }
}
