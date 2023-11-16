
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolutionsForBuisnesTestTask.DAL;
using SolutionsForBuisnesTestTask.DAL.Repositories;
using SolutionsForBuisnesTestTask.Domain.Models;
using SolutionsForBuisnesTestTask.Domain.Validators;
using SolutionsForBuisnesTestTask.Services.Implementations;
using SolutionsForBuisnesTestTask.Services.Interfaces;

namespace SolutionsForBuisnesTestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IBaseRepository<Order>, OrderRepository>();
            builder.Services.AddScoped<IBaseRepository<OrderItem>, OrderItemRepository>();
            builder.Services.AddScoped<IBaseRepository<Provider>, ProviderRepository>();
            builder.Services.AddScoped<IFilterService, FilterService>();
            builder.Services.AddScoped<IOrderCRUDService, OrderCRUDService>();
            builder.Services.AddScoped<IValidator<Order>, OrderValidator>();

            builder.Services.AddAntiforgery(x => x.HeaderName = "__RequestVerificationToken");
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