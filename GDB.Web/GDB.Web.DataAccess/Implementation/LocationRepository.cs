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
    public class LocationRepository : ILocationRepository
    {
        private readonly ILogger<CategoryRepository> logger;
        private GDBContext DbContext { get; set; }

        public LocationRepository(GDBContext _DbContext, ILogger<CategoryRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        
        public async Task<bool> Add(LocationViewModel locationViewModel)
        {
            try
            {
                var location = new Location
                {
                    UserId = 1,                    
                    LocationDescription = locationViewModel.LocationName,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };
                DbContext.Locations.Add(location);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<bool> Update(LocationViewModel locationViewModel)
        {
            try
            {
                var existingLocationData = (DbContext.Locations.SingleOrDefault(x => x.LocationId == locationViewModel.LocationId));
                
                if (existingLocationData == null)
                {
                    logger.LogWarning("Location with ID {LocationId} not found.", locationViewModel.LocationId);
                    return false;
                }
                if (existingLocationData != null)
                {
                    existingLocationData.LocationDescription = locationViewModel.LocationName;
                    existingLocationData.UserId = 1;
                    existingLocationData.ModifiedDate = DateTime.UtcNow; 
                }

                DbContext.Locations.Update(existingLocationData);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<List<LocationViewModel>> GetAllLocations()
        {
            try
            {
                var locations = await DbContext.Locations
                    .AsNoTracking()
                    .OrderBy(x => x.LocationDescription)
                    .Select(x => new LocationViewModel
                    {
                        LocationId = x.LocationId,
                        LocationName = x.LocationDescription
                    })
                    .ToListAsync();

                if (locations == null || locations.Count == 0)
                {
                    logger.LogInformation("No locations found in the database.");
                    return new List<LocationViewModel>();
                }
                return locations;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all locations.");
                return new List<LocationViewModel>();
            }
        }
    }
}
