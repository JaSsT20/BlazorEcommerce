global using BlazorEcommerce.Shared;
global using Microsoft.EntityFrameworkCore;
global using BlazorEcommerce.Server.Data;
global using BlazorEcommerce.Server.Services.ProductService;
using Microsoft.AspNetCore.ResponseCompression;


var builder = WebApplication.CreateBuilder(args);

//GettingConSTR
var ConStr = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(ConStr));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//---------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//---------------------------------------------

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseSwaggerUI(); //SwaggerUserInterface

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger(); //Usw Swagger

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
