using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_2_Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database setup (SQLite)
            builder.Services.AddDbContext<MatrixIncDbContext>(
                options => options.UseSqlite("Data Source=MatrixInc.db"));

            // MVC
            builder.Services.AddControllersWithViews();

            // Dependency injection: repositories
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPartRepository, PartRepository>();

            // Authentication & Authorization (cookies)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Error handling
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Database initialisatie
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MatrixIncDbContext>();
                context.Database.EnsureCreated();
                MatrixIncDbInitializer.Initialize(context);
            }

            // Middleware volgorde
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Belangrijk: NA UseRouting
            app.UseAuthorization();

            // Route mapping
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
