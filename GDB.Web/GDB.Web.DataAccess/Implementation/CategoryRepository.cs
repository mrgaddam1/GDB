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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> logger;
        private GDBContext DbContext { get; set; }

        public CategoryRepository(GDBContext _DbContext, ILogger<CategoryRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<bool> Add(CategoryViewModel categoryViewModel)
        {
            try
            {
                var category = new Category
                {
                    UserId = 1,
                    CategoryName = categoryViewModel.CategoryDescription,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };
                DbContext.Categories.Add(category);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            try
            {
                var categorys = await DbContext.Categories
                    .AsNoTracking()
                    .OrderBy(x => x.CategoryName)
                    .Select(x => new CategoryViewModel
                    {
                        CategoryId = x.CategoryId,
                        CategoryDescription = x.CategoryName
                    })
                    .ToListAsync();

                if (categorys == null || categorys.Count == 0)
                {
                    logger.LogInformation("No Category data found in the database.");
                    return new List<CategoryViewModel>();
                }
                return categorys;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all locations.");
                return new List<CategoryViewModel>();
            }
        }

        public async Task<bool> Update(CategoryViewModel categoryViewModel)
        {
            try
            {
                var existingCategoryData = (DbContext.Categories.SingleOrDefault(x => x.CategoryId == categoryViewModel.CategoryId));

                if (existingCategoryData != null)
                {
                    existingCategoryData.UserId = 1;
                    existingCategoryData.CategoryName = categoryViewModel.CategoryDescription;
                    existingCategoryData.CreatedDate = existingCategoryData.CreatedDate;
                    existingCategoryData.ModifiedDate = DateTime.Now;
                }

                DbContext.Categories.Update(existingCategoryData);
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
