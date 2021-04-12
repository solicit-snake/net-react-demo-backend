using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using net_react_demo_backend.Models;
using net_react_demo_backend.Services;

namespace net_react_demo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DbConnector _dbConnector;
        //TODO: This can potentially be added to the .env file.
        private string connectionStringName = "NetReactDemoCon";

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnector = new DbConnector(_configuration, connectionStringName);
        }

        [HttpGet("get-by-user-id/{userId}")]
        public JsonResult GetById(string userId)
        {   
            //Query to be executed
            string query = @"
                select UserId, FirstName, LastName, Email, Phone, Password from dbo.Users where UserId = " + userId + @"";

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpGet("get-by-email/{userEmail}")]
        public JsonResult GetByEmail(string userEmail)
        {
            //Query to be executed
            string query = @"
                select UserId, FirstName, LastName, Email, Phone, Password from dbo.Users where Email = '" + userEmail + @"'";

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpPost("post")]
        public JsonResult Post(User user)
        {
            //TODO: need to potentially b-crypt this password either before it's sent here, or even right here (probs before)
            //Query to be executed
            string query = @"
                insert into dbo.Users values ('"+user.FirstName+ @"', '" + user.LastName + @"', '" + user.Email + @"', '" + user.Phone + @"', '" + user.Password + "')";
            
            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpPut("put")]
        public JsonResult Put(User user)
        {
            //TODO: need to potentially b-crypt this password either before it's sent here, or even right here (probs before)
            //Query to be executed
            string query = @"
                update dbo.Users set 
                FirstName = '" + user.FirstName + @"', LastName = '" + user.LastName + @"',
                Email = '" + user.Email + @"', Phone ='" + user.Phone + @"', Password ='" + user.Password + @"'
                where UserID = "+ user.UserId +@"";
            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpDelete("delete-by-user-id/{userId}")]
        public JsonResult Delete(int userId)
        {
            //Query to be executed
            string query = @"
                delete from dbo.Users 
                where UserId = "+ userId +@"";

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }
    }
}
