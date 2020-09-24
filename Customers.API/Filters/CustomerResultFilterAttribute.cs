﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Customers.API.Filters
{
    public class CustomerResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if(result?.Value == null || result.StatusCode > 200 || result.StatusCode >= 300)
            {
                await next();
                return;
            }

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

            result.Value = mapper.Map<Models.Customer>(result.Value);

            await next();
        }
    }
}
