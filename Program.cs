using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Examenes.Data;
using Examenes.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<YaPedidosContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("YaPedidosContext") ?? throw new InvalidOperationException("Connection string 'YaPedidosContext' not found.")));

// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>() //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()  //Se agrega para poder hacer el manejo de roles
    .AddEntityFrameworkStores<YaPedidosContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.MapRazorPages();
app.Run();
