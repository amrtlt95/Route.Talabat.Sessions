
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Extentions;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.SqlServerDbFiles.Data;
using Route.Talabat.Infrastructure.SqlServerDbFiles.Identity;
using StackExchange.Redis;

namespace Route.Talabat.APIs
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Adding Services
			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddSwaggerServices();
			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies();
			});


			webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
			});



			webApplicationBuilder.Services.AddScoped<IConnectionMultiplexer>((serviceProvider) => {
				var connection = webApplicationBuilder.Configuration.GetConnectionString("Radis");
				return ConnectionMultiplexer.Connect(connection);
            });
            webApplicationBuilder.Services.AddApplicationServices();
            #endregion

            var app = webApplicationBuilder.Build();

			#region Applying Any Pending Migrations
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<ApplicationDbContext>();
			var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await _dbContext.Database.MigrateAsync();
				await ApplicationContextSeed.SeedAsync(_dbContext);

				await _identityDbContext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error occured during applying the Migrations");
			}
			#endregion

			// Configure the HTTP request pipeline.
			#region Configure Kestrel Middlewares
			app.UseMiddleware<ExceptionMiddleware>();
			if (app.Environment.IsDevelopment())
			{
				app.AddSwaggerMiddlewares();

            }
			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
