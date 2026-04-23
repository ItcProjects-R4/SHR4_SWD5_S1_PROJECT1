using Core.IReprosatory;
using Core.IServices;
using Core.Models;
using Infrastructure.Context;
using Infrastructure.Reprosatory;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Resturant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================
            // Controllers
            // =========================
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            // =========================
            // DB Context
            // =========================
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });

            // =========================
            // Identity
            // =========================
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            // =========================
            // JWT Authentication
            // =========================
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                    )
                };
            });

            // =========================
            // Repositories
            // =========================
            builder.Services.AddScoped<IItemRepo, ItemRepo>();
            builder.Services.AddScoped<ICatrgoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IcountryRepo, CountryRepo>();
            builder.Services.AddScoped<IOrderItemsRepo, OrderItemsRepo>();
            builder.Services.AddScoped<IDineinOrderRepo, DineinOrderRepo>();
            builder.Services.AddScoped<IDeliveryOrderRepo, DelivaryOrderRepo>();


            // =========================
            // Services
            // =========================
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IItemService, IteamServices>();
            builder.Services.AddScoped<IOrderItemsService, OredrItemsServices>();
            builder.Services.AddScoped<IDieninOrderService, DineinOredrServices>();
            builder.Services.AddScoped<ITakeAwayOrderService, TakeAwayOredrServices>();
            builder.Services.AddScoped<IDelivaryOrderService, DeliveryOredrServices>();
            var paymobSettings = builder.Configuration
             .GetSection("PaymobSettings")
             .Get<PaymobSettings>();

            builder.Services.AddSingleton(paymobSettings);

            builder.Services.AddScoped<IPaymobService, PaymobService>();
            builder.Services.AddHttpClient<PaymobService>();

            // =========================
            // Swagger
            // =========================
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // =========================
            // Middleware
            // =========================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}