﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure.BasketRepository;
using Route.Talabat.Infrastructure.GenericRepository;
using StackExchange.Redis;

namespace Route.Talabat.APIs.Extentions
{
    public static class AddApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(options =>
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
            
            return services;
        }
    }
}
