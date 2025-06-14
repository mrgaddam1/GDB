﻿using GDB.Web.Core.Models;
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
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> logger;
        private GDBContext DbContext { get; set; }

        public ProductRepository(GDBContext _DbContext, ILogger<ProductRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<bool> Add(ProductViewModel productViewModel)
        {
            try
            {
                var product = new Product
                {
                    UserId = 1,
                    CategoryId = productViewModel.CategoryId,
                    SubcategoryId = productViewModel.SubCategoryId,
                    ProductName = productViewModel.ProductName,
                    VendorId = productViewModel.VendorId,
                    FoodPackingTypeId = productViewModel.FoodPackingTypeId,
                    //Quantity = productViewModel.Quantity,
                    ProductPrice = productViewModel.ProductPrice,
                    PurchasedDate = productViewModel.PurchasedDate,
                    CreatedDate = DateTime.UtcNow,
                    Modifieddate = null
                };
                DbContext.Products.Add(product);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            try
            {
                var products = await(from p in DbContext.Products
                                join c in DbContext.Categories on p.CategoryId equals c.CategoryId
                                join s in DbContext.SubCategories on p.SubcategoryId equals s.SubCategoryId
                                join l in DbContext.Vendors on p.VendorId equals l.VendorId
                                select new ProductViewModel
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    CategoryName = c.CategoryName,
                                    SubCategoryName = s.SubCategoryName,
                                    VendorName = l.VendorName,
                                    //Quantity = p.Quantity,
                                    ProductPrice = p.ProductPrice,
                                    FoodPackingTypeId = p.FoodPackingTypeId,
                                    PurchasedDate = p.PurchasedDate,

                                })
                    .OrderBy(x => x.ProductName)
                    .ToListAsync();


                if (products == null || products.Count ==0)
                {
                    logger.LogInformation("No Products found in the database.");
                    return new List<ProductViewModel>();
                }
                return products;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all locations.");
                return new List<ProductViewModel>();
            }
        }

        public async Task<bool> Update(ProductViewModel productViewModel)
        {
            try
            {
                var existingProductData = (DbContext.Products.SingleOrDefault(x => x.ProductId == productViewModel.ProductId));

                if (existingProductData != null)
                {                   
                    existingProductData.CategoryId = existingProductData.CategoryId;
                    existingProductData.SubcategoryId = existingProductData.SubcategoryId;
                    existingProductData.UserId = 1;
                    existingProductData.VendorId = existingProductData.VendorId;
                    existingProductData.ProductName = productViewModel.ProductName;
                    existingProductData.FoodPackingTypeId = existingProductData.FoodPackingTypeId;

                    if (productViewModel.PurchasedDate == existingProductData.PurchasedDate)
                    {
                        //existingProductData.Quantity = productViewModel.Quantity;
                        existingProductData.ProductPrice = productViewModel.ProductPrice;
                    }
                    else
                    {
                        //existingProductData.Quantity = existingProductData.Quantity + productViewModel.Quantity;
                        existingProductData.ProductPrice = existingProductData.ProductPrice + productViewModel.ProductPrice;
                    }
                   
                    existingProductData.PurchasedDate = productViewModel.PurchasedDate;
                    existingProductData.CreatedDate = existingProductData.CreatedDate;
                    existingProductData.Modifieddate = DateTime.Now;   
                }
                DbContext.Products.Update(existingProductData);
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
