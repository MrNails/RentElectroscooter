
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentElectroScooter.API.Services;
using RentElectroScooter.DAL.Repositories;
using System.Diagnostics;
using System.Text;

namespace RentElectroScooter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration[Constants.JWT_Issuer],
                    ValidAudience = builder.Configuration[Constants.JWT_Audience],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[Constants.JWT_Key])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            builder.Services.AddTransient(serviceProvider =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder();

                dbContextOptionsBuilder.UseSqlServer(builder.Configuration["ConnectionStrings:Production"])
                        .LogTo(str => Debug.WriteLine(str));

                return new RentElectroscooterDBContext(dbContextOptionsBuilder.Options, AvailableDatabases.MSSQL);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}