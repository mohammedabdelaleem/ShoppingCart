using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Data;
using MyShop.DataAccess.Implementations;
using MyShop.Entities.Repositories;

namespace MyShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
         

            builder.Services.AddScoped<IUnitIfWork, UnitOfWork>();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            //Connection To DB
            builder.Services.AddDbContext<AppDbContext>(
                options =>
                {
                    string constr = builder.Configuration.GetConnectionString("constr")!;
                    options.UseSqlServer(constr, b => b.MigrationsAssembly("MyShop.DataAccess"));
                });


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
                name: "Admin",
                pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
              name: "default",
              pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
