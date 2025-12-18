using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GDB.Web.DataAccess.Implementation
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly ILogger<InvestmentRepository> logger;
        private GDBContext DbContext { get; set; }

        public InvestmentRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<InvestmentOptionCategoryViewModel>> GetAllInvestmentCategories()
        {
            try
            {
                var investmentCategories = await DbContext
                                                .InvestmentOptionCategories
                                                .OrderBy(x => x.InvestmentOptionCategoryId)
                                                .Select(x => new InvestmentOptionCategoryViewModel
                                                {
                                                    InvestmentOptionCategoryId = x.InvestmentOptionCategoryId,
                                                    InvestmentOptionCategoryDescription = x.InvestmentOptionCategoryDescription
                                                })
                                                .ToListAsync();

                if (investmentCategories == null || investmentCategories.Count == 0)
                {
                    logger.LogInformation("No locations found in the database.");
                    return new List<InvestmentOptionCategoryViewModel>();
                }
                return investmentCategories;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all investment Categories.");
                return new List<InvestmentOptionCategoryViewModel>();
            }
        }

        public async Task<List<InvestmentOptionSubCategoryViewModel>> GetAllInvestmentSubCategories()
        {
            try
            {
                var investmentSubCategories = await DbContext
                                                .InvestmentOptionSubCategories
                                                .OrderBy(x => x.InvestmentSubCategoryId)
                                                .Select(x => new InvestmentOptionSubCategoryViewModel
                                                {
                                                    InvestmentSubCategoryId = x.InvestmentSubCategoryId,
                                                    InvestmentOptionCategoryId = x.InvestmentOptionCategoryId,
                                                    InvestmentSubCategoryDescription = x.InvestmentSubCategoryDescription
                                                })
                                                .ToListAsync();

                if (investmentSubCategories == null || investmentSubCategories.Count == 0)
                {
                    logger.LogInformation("No locations found in the database.");
                    return new List<InvestmentOptionSubCategoryViewModel>();
                }
                return investmentSubCategories;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all investment Sub Categories.");
                return new List<InvestmentOptionSubCategoryViewModel>();
            }
        }

        public async Task<List<InvestmentViewModel>> GetAllInvestmentDetails()
        {
            try
            {
                var investmentData = await (from i in DbContext.InvestmentDetails
                                      join ic in DbContext.InvestmentOptionCategories on i.InvestmentOptionId equals ic.InvestmentOptionCategoryId
                                      join isc in DbContext.InvestmentOptionSubCategories on i.InvestmentSubCategoryId equals isc.InvestmentSubCategoryId
                                      select new InvestmentViewModel
                                      {
                                          InvestmentId = i.InvestmentId,
                                          InvestmentCategoryId = ic.InvestmentOptionCategoryId,
                                          InvestmentOptionCategoryDescription = ic.InvestmentOptionCategoryDescription,
                                          InvestmentSubCategoryId = isc.InvestmentSubCategoryId,
                                          InvestmentSubCategoryDescription = isc.InvestmentSubCategoryDescription,
                                          InvestedAmount = i.InvestedAmount,
                                          InvestedDate = i.InvestedDate,
                                          Descrpition = i.Descrpition
                                      }).OrderByDescending(x => x.InvestmentCategoryId).ToListAsync();

                if (investmentData == null || investmentData.Count == 0)
                {
                    logger.LogInformation("No locations found in the database.");
                    return new List<InvestmentViewModel>();
                }
                return investmentData;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all investment Sub Categories.");
                return new List<InvestmentViewModel>();
            }
        }

        public async Task<bool> AddInvestment(InvestmentViewModel investmentViewModel)
        {
            try
            {
                var investment = new InvestmentDetail
                {
                    UserId = 1,
                    InvestedAmount = investmentViewModel.InvestedAmount,
                    InvestedDate = investmentViewModel.InvestedDate,
                    InvestmentOptionId = investmentViewModel.InvestmentCategoryId,
                    InvestmentSubCategoryId = investmentViewModel.InvestmentSubCategoryId,
                    Descrpition = investmentViewModel.Descrpition,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };
               
                DbContext.InvestmentDetails.Add(investment);
                await DbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }

        public async Task<bool> UpdateInvestment(InvestmentViewModel investmentViewModel)
        {
            try
            {
                var existingInvestment = await DbContext.InvestmentDetails.FindAsync(investmentViewModel.InvestmentId);
                if (existingInvestment == null)
                {
                    logger.LogWarning("Investment with ID {InvestmentId} not found.", investmentViewModel.InvestmentId);
                    return false;
                }
                existingInvestment.InvestedAmount = investmentViewModel.InvestedAmount;
                existingInvestment.InvestedDate = investmentViewModel.InvestedDate;
                existingInvestment.InvestmentOptionId = investmentViewModel.InvestmentCategoryId;
                existingInvestment.InvestmentSubCategoryId = investmentViewModel.InvestmentSubCategoryId;
                existingInvestment.Descrpition = investmentViewModel.Descrpition;
                existingInvestment.ModifiedDate = DateTime.UtcNow;
                DbContext.InvestmentDetails.Update(existingInvestment);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return false;
            }
        }   

        public async Task<bool> VerifySecurityChecks(SecurityCheckViewModel securityCheckViewModel)
        {
             
            bool securityCheckStatus = true;

            //var fullName = BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.FullName);
            //var mobileNumber = BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.MobileNumber);
            //var user12DigitsPasscode = BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.Security12DigitsPasscode);
            //var user6DigitsPincode = BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.Security6DigitsPincode);


            securityCheckStatus = (await DbContext
                                        .InvestmentSecurityChecks
                                        .AnyAsync(s => s.FullName == BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.FullName) &&
                                                       s.MobileNumber == BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.MobileNumber) &&
                                                       s.Security12DigitsPasscode == BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.Security12DigitsPasscode) &&
                                                       s.Security6DigitsPincode == BCrypt.Net.BCrypt.HashPassword(securityCheckViewModel.Security6DigitsPincode))

            );
            if (!securityCheckStatus)
            {
                logger.LogWarning("Security check failed for user with Full Name: {FullName}", securityCheckViewModel.FullName);
                return securityCheckStatus;
            }
            return securityCheckStatus;
        }

        public async Task<bool> AddInvestmentSummary(InvestmentSummaryViewModel investmentSummaryViewModel)
        {
            try
            {
                var investmentSummary = new InvestmentSummary
                {
                    UserId = 1,   
                    InvestmentOptionCategoryId = investmentSummaryViewModel.InvestmentOptionCategoryId,
                    InvestmentSubCategoryId = investmentSummaryViewModel.InvestmentSubCategoryId,
                    Descrpition = investmentSummaryViewModel.Descrpition,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null
                };

                DbContext.InvestmentSummaries.Add(investmentSummary);
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
