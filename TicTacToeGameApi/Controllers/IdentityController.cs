using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLDatabase;

namespace TicTacToeGameApi.Controllers
{
    [Route("api/Identity")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/Identity
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Welcome" };
        }
        [HttpPost]
        [Log]
        public void Post([FromBody] Users users)
        {
            Database database = new Database();
            string accessToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bool result = database.AddUsers(users.FirstName, users.LastName, users.UserName, accessToken);
        }

    
    }
}
