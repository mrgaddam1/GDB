namespace GDB.Web.Common.Helpers
{
    public static class ApiRoutes
    {

        public static class Investments
        {
            public const string Base = "api/Investment/";
            public const string GetAllInvestmentCategories = "GetAllInvestmentCategories";
            public const string GetAllInvestmentSubCategories = "GetAllInvestmentSubCategories";

            public const string AddInvestment = "Add";
            public const string UpdateInvestment = "Update";
            public const string DeleteInvestment = "Delete";
            public const string GetAllBusinessInvestmentsById = "GetAllInvestmentDetailsById";
            public const string GetAllInvestments = "GetAllInvestments";
            public const string VerifySecurityCheck = "VerifySecurityCheck";
        }
       
    }
}
