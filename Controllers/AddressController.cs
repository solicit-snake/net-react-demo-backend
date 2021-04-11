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
    public class AddressController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet ("get-by-address-id/{addressId}")]
        public JsonResult GetByAddressId(string addressId)
        {
            string query = @"
            select UserId, Street, Suburb, State, Postcode, Country
            from dbo.Addresses where AddressId = " + addressId;

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
    }
}
