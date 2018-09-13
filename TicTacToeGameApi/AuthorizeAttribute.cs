using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLDatabase;

namespace TicTacToeGameApi
{
    public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["accessToken"].ToString();
            if(String.IsNullOrEmpty(accessToken))
                throw new UnauthorizedAccessException("No Access Token passed");
            else
            {
                Database database = new Database();
                bool checkAccessToken = database.CheckAccessToken(accessToken);
                if (checkAccessToken == false)
                    throw new UnauthorizedAccessException("Access Token doesn't exist");
            }
        }
    }
}
