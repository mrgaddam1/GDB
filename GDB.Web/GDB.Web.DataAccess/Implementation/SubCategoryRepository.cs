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
        public async Task<bool> Add(SubCategoryViewModel subCategoryViewModel)
        {
            try
            {
                var subCategory = new SubCategory
                {
                    UserId = 1,
                    CategoryId = subCategoryViewModel.CategoryId,
                    SubCategoryName = subCategoryViewModel.SubCategoryDescription,
                    CreatedDate = DateTime.UtcNow,
                    Modifieddate = null
                };
                DbContext.SubCategories.Add(subCategory);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
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

        public async Task<bool> Update(SubCategoryViewModel subCategoryViewModel)
        {
            try
            {
                var existingSubCategoryData = (DbContext.SubCategories.SingleOrDefault(x => x.SubCategoryId == subCategoryViewModel.SubCategoryId));

                if (existingSubCategoryData != null)
                {
                    existingSubCategoryData.UserId = 1;
                    existingSubCategoryData.CategoryId = existingSubCategoryData.CategoryId;
                    existingSubCategoryData.SubCategoryName = subCategoryViewModel.SubCategoryDescription;
                    existingSubCategoryData.CreatedDate = existingSubCategoryData.CreatedDate;
                    existingSubCategoryData.Modifieddate = DateTime.Now;
                }

                DbContext.SubCategories.Update(existingSubCategoryData);
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
