using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
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

        public StaterRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<StatersViewModel>> GetAllStaters()
        {
            var statersData = new List<StatersViewModel>();
            statersData = await (from s in DbContext.Staters.AsNoTracking()
                                 select new StatersViewModel
                                 {
                                     StaterId = s.StaterId,
                                     StarterDescription = s.StaterDescription
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
    }
}
