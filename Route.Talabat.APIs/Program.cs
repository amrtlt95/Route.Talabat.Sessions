
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;

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
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies();
			}); 
			webApplicationBuilder.Services.AddScoped( typeof(IGenericRepository<>) , typeof(GenericRepository<>));
			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));
			webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (action) =>
				{
					var errors = action.ModelState
					.Where(a => a.Value.Errors.Count() > 0)
					.SelectMany(a => a.Value.Errors)
					.Select(a => a.ErrorMessage)
					.ToList();

					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);

				};
			});
			#endregion

			var app = webApplicationBuilder.Build();

			#region Applying Any Pending Migrations
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<ApplicationDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await _dbContext.Database.MigrateAsync();
				await ApplicationContextSeed.SeedAsync(_dbContext);
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
				app.UseSwagger();
				app.UseSwaggerUI();
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
