using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLDatabase;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TicTacToeGameApi
{
    public class LogAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Database database = new Database();
            JObject result = null;
            try
            {
                result = JObject.Parse(JsonConvert.SerializeObject(context.Result));
            }
            catch { }
            string request = context.HttpContext.Request.Method + " on " + context.HttpContext.Request.Path;
            string exception = string.Empty;
            if (context.Exception != null)
            {
                exception = context.Exception.Message;
            }
            JToken token = null;
            if (result != null)
                result.TryGetValue("Value", out token);
            string response = string.Empty;
            if (token != null)
                response = token.ToString();
            DateTime Date = DateTime.Now;

            database.AddLog(request, response, exception, Date);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
