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
            var subCategoryViewModel = new List<SubCategoryViewModel>();
            try
            {
                subCategoryViewModel = await (from sc in DbContext.SubCategories.AsNoTracking()
                                           join c in DbContext.Categories.AsNoTracking()
                                           on sc.CategoryId equals c.CategoryId
                                           select new SubCategoryViewModel
                                           {
                                               SubCategoryId = sc.SubCategoryId,
                                               SubCategoryDescription = sc.SubCategoryName,
                                               CategoryId = sc.CategoryId,
                                               CategoryDescription = c.CategoryName,
                                           }
                                           ).OrderBy(x => x.SubCategoryDescription).ToListAsync();
                    

                if (subCategoryViewModel == null || subCategoryViewModel.Count == 0)
                {
                    logger.LogInformation("No Subcategy data found in the database.");
                    return new List<SubCategoryViewModel>();
                }
                return subCategoryViewModel;
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
