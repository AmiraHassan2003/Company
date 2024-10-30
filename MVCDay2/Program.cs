using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCDay2.Models.Context;
using MVCDay2.Repository.Employee;

namespace MVCDay2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<NewCompanyContext>(
                OptionsBuilder =>
                {
                    OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("myConn"));
                }
                );
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
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
                pattern: "{controller=Company}/{action=GetAll}/{id?}");

            app.Run();
        }
    }
}
