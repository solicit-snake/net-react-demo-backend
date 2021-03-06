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
    public class AddressController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DbConnector _dbConnector;
        //TODO: This can potentially be added to the .env file.
        private string connectionStringName = "NetReactDemoCon";
        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnector = new DbConnector(_configuration, connectionStringName);
        }

        [HttpGet("get-by-address-id/{addressId}")]
        public JsonResult GetByAddressId(string addressId)
        {
            string query = @"
            select UserId, Street, Suburb, State, Postcode, Country
            from dbo.Addresses where AddressId = " + addressId;

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpGet("get-by-user-id/{userId}")]
        public JsonResult GetByUserId(string userId)
        {
            string query = @"
            select AddressId, UserId, Street, Suburb, State, Postcode, Country
            from dbo.Addresses where UserId = " + userId;

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpPost("post")]
        public JsonResult Post(Address address)
        {
            string query = @"
            insert into dbo.Addresses values 
            (" + address.UserId + @",  '" + address.Street + @"', '" + address.Suburb + @"', '" + address.State + @"', '" + address.Postcode + @"', '" + address.Country + @"')";

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpPut("put")]
        public JsonResult Put(Address address)
        {
            string query = @"
            update dbo.Addresses set
            Street = '" + address.Street + @"', Suburb = '" + address.Suburb + @"', State = '" + address.State + @"',
            Postcode = '" + address.Postcode + @"', Country = '" + address.Country + @"' 
            where AddressId = " + address.AddressId + @"";

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        [HttpDelete("delete-by-address-id/{addressId}")]
        public JsonResult DeleteByAddressId(int addressId)
        {
            string query = @"
                delete from dbo.Addresses 
                where AddressId = " + addressId + @"";

            _dbConnector.setQuery(query);
            return (_dbConnector.runQuery());
        }

        //TODO: rest of address methods.
    }
}
