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
        public Task<bool> Add(CategoryViewModel categoryViewModel)
        {
            throw new NotImplementedException();
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

        public Task<bool> Update(CategoryViewModel categoryViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
