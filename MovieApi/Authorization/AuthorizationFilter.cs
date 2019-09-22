using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Authorization
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["Authorization"];

            if (apiKey.Any())
            {
                var subStrings = apiKey.ToString().Split(" ");
                if (!(subStrings[0] == "apiKey" && subStrings[1].Any()))
                {
                    context.Result = new StatusCodeResult(410);
                }
            }
            else
            {
                context.Result = new StatusCodeResult(410);
            }
        }
    }
}
