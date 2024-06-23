using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Repository;
using MySql.Data;
using Pomelo.EntityFrameworkCore.MySql;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace OnlineShopProject.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:7013",
                        ValidAudience = "https://localhost:7013",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SigningSecuritykey123456789_./5657"))

                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireLoggedIn", policy =>
                    policy.RequireAuthenticatedUser());
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AppPolicy", app =>
                {
                    app.WithOrigins("https://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            builder.Services.AddDbContext<DataContext>(options =>
            {
                var connectionString =  System.Environment.GetEnvironmentVariable("ONLINESHOPCONNECTIONSTRING");
                
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
         
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AppPolicy");

            app.UseAuthorization();

            app.UseAuthentication();   

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
