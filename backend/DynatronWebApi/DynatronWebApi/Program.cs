// Ignore Spelling: Dynatron

using Carter;
using DynatronWebApi.Behaviours;
using DynatronWebApi.Database;
using DynatronWebApi.Seeding;
using DynatronWebApi.UoW;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynatronWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            var assembly = typeof(Program).Assembly;

            builder.Services.AddCarter();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            builder.Services.AddValidatorsFromAssembly(assembly);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        sqlServerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }
                )
            );
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowProductionOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader() // Allow any header
                        .AllowAnyMethod() // Allow any HTTP method
                        .AllowCredentials(); // Allow credentials
                });

                options.AddPolicy("AllowAll", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true) // Allow all origins
                        .AllowAnyHeader() // Allow any header
                        .AllowAnyMethod() // Allow any HTTP method
                        .AllowCredentials(); // Allow credentials
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply migrations and seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                // Apply migrations
                dbContext.Database.Migrate();

                // Seed data if necessary
                var seedData = new SeedData(dbContext);
                seedData.Seed();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.MapCarter();
            app.Run();
        }
    }
}