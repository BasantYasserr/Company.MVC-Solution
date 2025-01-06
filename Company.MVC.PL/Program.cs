using Company.MVC.BLL.Interfaces;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Company.MVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();   // Adds the built-in services for MVC

            //builder.Services.AddScoped<AppDbContext>();   // Allow DI For AppDbContext / Register the AppDbContext with the DI container
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  // Use SQL Server
            }); // Extension Method: Allow DI For AppDbContext / Register the AppDbContext with the DI container
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();  // Allow DI For DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  // Allow DI For EmployeeRepository 

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
        }
    }
}