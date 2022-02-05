using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApi4.Filters
{
    public class HeaderFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string date = DateTimeOffset.UtcNow.ToString();
            context.HttpContext.Response.Headers.Add("Response", date);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string date = DateTimeOffset.UtcNow.ToString();
            context.HttpContext.Response.Headers.Add("Request", date);
        }
    }
}
