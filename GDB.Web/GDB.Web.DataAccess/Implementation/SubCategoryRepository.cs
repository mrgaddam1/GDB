using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ILogger<SubCategoryRepository> logger;
        private GDBContext DbContext { get; set; }

        public SubCategoryRepository(GDBContext _DbContext, ILogger<SubCategoryRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public Task<bool> Add(SubCategoryViewModel subCategoryViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SubCategoryViewModel>> GetAll()
        {
            try
            {
                var subCategories = await DbContext.SubCategories
                    .AsNoTracking()
                    .OrderBy(x => x.SubCategoryName)
                    .Select(x => new SubCategoryViewModel
                    {
                        SubCategoryId = x.SubCategoryId,
                        SubCategoryDescription = x.SubCategoryName
                    })
                    .ToListAsync();

                if (subCategories == null || subCategories.Count == 0)
                {
                    logger.LogInformation("No Subcategy data found in the database.");
                    return new List<SubCategoryViewModel>();
                }
                return subCategories;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all Subcategy data.");
                return new List<SubCategoryViewModel>();
            }
        }

        public Task<bool> Update(SubCategoryViewModel subCategoryViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
