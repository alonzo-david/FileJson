using FileJson.Interfaces;
using FileJson.Models;
using FileJson.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<FileContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

builder.Services.AddTransient<IFileUploadService, FileUploadService>();
builder.Services.AddTransient<IEmpresaService, EmpresaService>();

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

app.Run();
