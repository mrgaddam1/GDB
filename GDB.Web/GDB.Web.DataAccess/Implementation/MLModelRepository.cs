using GDB.Web.Common.Extensions;
using GDB.Web.Common.Helpers;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;
using static Microsoft.ML.ForecastingCatalog;
using static TorchSharp.torch.utils;

namespace GDB.Web.DataAccess.Implementation
{
    public class MLModelRepository : IMLModelRepository
    {
        private readonly ILogger<LocationRepository> logger;
        private GDBContext DbContext { get; set; }

        private readonly MLContext mlContext;

        private ITransformer model;
        public MLModelRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
            mlContext = new MLContext();
        }
      
        public async Task<List<OrderPredictions>> TrainModelAndPredictSales()
        {
            var orderPredictionList = new List<OrderPredictions>();
             List<SalesData> salesData = new List<SalesData>();


            salesData = (from s in DbContext.ExpensesAndSales
                         select new SalesData
                         {
                             Month = (float)(s.WeekId.Value),
                             Sales = (float)Math.Floor(s.TotalSales.Value),
                         }).OrderBy(s => s.Month).ToList();
                       
            IDataView dataView = mlContext.Data.LoadFromEnumerable(salesData);
       
            var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                                        outputColumnName: "ForecastedSales",
                                        inputColumnName: "Sales",
                                        windowSize:  3,               //Math.Max(2, salesData.Count / 3),  // Adjust dynamically
                                        seriesLength: salesData.Count,
                                        trainSize: salesData.Count,
                                        horizon: 11,
                                        confidenceLevel: 0.95f,
                                        confidenceLowerBoundColumn: "LowerBoundSales",
                                        confidenceUpperBoundColumn: "UpperBoundSales");


            model = forecastingPipeline.Fit(dataView);

            // Create a prediction engine
            TimeSeriesPredictionEngine<SalesData, SalesForecast> forecastingEngine =
                                    model.CreateTimeSeriesEngine<SalesData, SalesForecast>(mlContext);

            // Predict future sales
            SalesForecast forecast = forecastingEngine.Predict();
         
            // Print the forecasted sales
            Console.WriteLine("Sales Forecast:");
            for (int i = 0; i < forecast.ForecastedSales.Length; i++)
            {
                Console.WriteLine($"Month: { salesData.Count + i + 1}, Forecasted Sales: { forecast.ForecastedSales[i]}");

                var orderPredictions = new OrderPredictions()
                {
                    Sno = i + 1,
                    WeekId = salesData.Count + i + 1,
                    Sales = forecast.ForecastedSales[i],
                };
                orderPredictionList.Add(orderPredictions);

            }
            return orderPredictionList;
        }

        public async Task<List<ExpensesPredictions>> TrainModelAndPredictExpenses()
        {
            var expensesPredictions = new List<ExpensesPredictions>();
            List<ExpensesData> expensesData = new List<ExpensesData>();

             
            expensesData = (from s in DbContext.ExpensesAndSales
                         select new ExpensesData
                         {
                             Month = (float)(s.WeekId.Value),
                             Expenses = (float)Math.Floor(s.TotalExpenses + ((s.TotalExpenses * 30)/100)),
                         }).OrderBy(s => s.Month).ToList();

            IDataView dataView = mlContext.Data.LoadFromEnumerable(expensesData);

            var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                                        outputColumnName: "ForecastedExpenses",
                                        inputColumnName: "Expenses",
                                        windowSize: 3,               //Math.Max(2, salesData.Count / 3),  // Adjust dynamically
                                        seriesLength: expensesData.Count,
                                        trainSize: expensesData.Count,
                                        horizon: 11,
                                        confidenceLevel: 0.95f,
                                        confidenceLowerBoundColumn: "LowerBoundSales",
                                        confidenceUpperBoundColumn: "UpperBoundSales");


            model = forecastingPipeline.Fit(dataView);

            // Create a prediction engine
            TimeSeriesPredictionEngine<ExpensesData, ExpensesForecast> forecastingEngine =
                                    model.CreateTimeSeriesEngine<ExpensesData, ExpensesForecast>(mlContext);

            // Predict future sales
            ExpensesForecast forecast = forecastingEngine.Predict();

            // Print the forecasted sales
            Console.WriteLine("Expenses Forecast:");
            for (int i = 0; i < forecast.ForecastedExpenses.Length; i++)
            {
                Console.WriteLine($"Month: {expensesData.Count + i + 1}, Forecasted Expenses: {forecast.ForecastedExpenses[i]}");

                var expensesPrediction = new ExpensesPredictions()
                {
                    Sno = i+1,
                    WeekId = expensesData.Count + i + 1,
                    Expenses = forecast.ForecastedExpenses[i],
                };
                expensesPredictions.Add(expensesPrediction);

            }
            return expensesPredictions;

        }

        private int? GetMaxWeekNumber()
        {
            var weekData = (from w in DbContext.WeekData select w).SingleOrDefault();
            int? weekId = weekData.WeekNumber;
            return weekId;
        }

        public async Task<List<OrderPredictions>> GetExistingSalesData()
        {
            var orderPredictions = new List<OrderPredictions>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_GetMaxSalesByWeek" , null);
                orderPredictions = ConvertDataTableToGenericList.ConvertDataTable<OrderPredictions>(data).
                                   OrderByDescending(x=>x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return orderPredictions;
        }
        public async Task<List<ExpensesPredictions>> GetExistingExpensesData()
        {
            var expensesPredictions = new List<ExpensesPredictions>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "UDP_GetMaxExpensesByWeek", null);
                expensesPredictions = ConvertDataTableToGenericList.ConvertDataTable<ExpensesPredictions>(data).
                                      OrderByDescending(x => x.WeekId).ToList(); 
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return expensesPredictions;

        }
    }
}
