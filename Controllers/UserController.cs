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

namespace net_react_demo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("get-by-id/{userId}")]
        public JsonResult GetById(string userId)
        {   
            //Query to be executed
            string query = @"
                select UserId, FirstName, LastName, Email, Phone, Password from dbo.Users where UserId = " + userId + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NetReactDemoCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                //Running the query here
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("get-by-email/{userEmail}")]
        public JsonResult GetByEmail(string userEmail)
        {
            //Query to be executed
            string query = @"
                select UserId, FirstName, LastName, Email, Phone, Password from dbo.Users where Email = '" + userEmail + @"'";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NetReactDemoCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                //Running the query here
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            //TODO: need to potentially b-crypt this password either before it's sent here, or even right here (probs before)
            //Query to be executed
            string query = @"
                insert into dbo.Users values ('"+user.FirstName+ @"', '" + user.LastName + @"', '" + user.Email + @"', '" + user.Phone + @"', '" + user.Password + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NetReactDemoCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                //Running the query here
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            //TODO: need to potentially b-crypt this password either before it's sent here, or even right here (probs before)
            //Query to be executed
            string query = @"
                update dbo.Users set 
                FirstName = '" + user.FirstName + @"', LastName = '" + user.LastName + @"',
                Email = '" + user.Email + @"', Phone ='" + user.Phone + @"', Password ='" + user.Password + @"'
                where UserID = "+ user.UserId +@"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NetReactDemoCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                //Running the query here
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(user.FirstName+" Was Updated Successfully");
        }

        [HttpDelete("{userId}")]
        public JsonResult Delete(int userId)
        {
            //Query to be executed
            string query = @"
                delete from dbo.Users 
                where UserId = "+ userId +@"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("NetReactDemoCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                //Running the query here
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Sucessfully");
        }
    }
}
