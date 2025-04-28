using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap5;
using GDB.Web.BLL.Implementation;
using GDB.Web.BLL.Interface;
using GDB.Web.Client.Pages;
using GDB.Web.Client.Security;
using GDB.Web.Components;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Radzen;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Radzen Services (optional, but helpful if using more components)
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services
    .AddBlazorise(options => { options.Immediate = true; })
    .AddBootstrap5Providers();

builder.Services.AddBlazoredSessionStorage();

RegisterCORS(builder);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<GDBContext>(options => options.EnableDetailedErrors(true));

 
RegisterHttpClient(builder);

ServiceDependency(builder);

//RegisterAuthenticationServices(builder);


var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GDB.Web.Client._Imports).Assembly);

app.Run();

static void ServiceDependency(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<AuthStateService>();
    builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IOrderTypesRepository, OrderTypesRepository>();
    builder.Services.AddScoped<IStaterRepository, StaterRepository>();
    builder.Services.AddScoped<IExpensesRepository, ExpensesRepository>();
    builder.Services.AddScoped<ILocationRepository, LocationRepository>();
    builder.Services.AddScoped<IAdvertiseSourceRepository, AdvertiseSourceRepository>();
    builder.Services.AddScoped<IGroceryRepository, GroceryRepository>();
    builder.Services.AddScoped<IStoreRepository, StoreRepository>();
    builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
    builder.Services.AddScoped<IMLModelRepository, MLModelRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
    builder.Services.AddScoped<IVendorRepository, VendorRepository>();

    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IOrdersTypeService, OrderTypesService>();
    builder.Services.AddScoped<IStaterService, StaterService>();
    builder.Services.AddScoped<IExpensesService, ExpensesService>();
    builder.Services.AddScoped<ILocationService, LocationService>();
    builder.Services.AddScoped<IAdvertiseSourceService, AdvertiseSourceService>();
    builder.Services.AddScoped<IStoreService, StoreService>();
    builder.Services.AddScoped<IGroceryService, GroceryService>();
    builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
    builder.Services.AddScoped<IMLModelService, MLModelService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITwilioService, TwilioService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IInventoryService, InventoryService>();
    builder.Services.AddScoped<IVendorService, VendorService>();
}

static void RegisterAuthenticationServices(WebApplicationBuilder builder)
{   
    builder.Services.AddAuthorizationCore();
    builder.Services.AddScoped<CustomAuthStateProvider>();
    builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
}

static void RegisterCORS(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder.WithOrigins("https://localhost:7040") // Add your Blazor app URL
                       .SetIsOriginAllowed((host) => true)
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
    });
}

static void RegisterHttpClient(WebApplicationBuilder builder)
{
    builder.Services.AddScoped(http => new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration.GetSection("BaseUrl").Value),
        Timeout = TimeSpan.FromMinutes(30)
    });
  
}