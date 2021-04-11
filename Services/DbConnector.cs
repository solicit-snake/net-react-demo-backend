using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace net_react_demo_backend.Services
{
    public class DbConnector
    {
        private readonly IConfiguration _configuration;
        private DataTable _table;
        private SqlDataReader _myReader;
        private string _connectionStringName;
        private string _sqlQuery;
        private int _rowsAffected;

        public DbConnector (IConfiguration configuration, string connectionStringName)
        {
            _configuration = configuration;
            _connectionStringName = connectionStringName;
            _table = new DataTable();
            _sqlQuery = "";
        }

        public void setQuery(string query)
        {
            _sqlQuery = query;
        }
        public  JsonResult runQuery()
        {
            string sqlDataSource = _configuration.GetConnectionString(_connectionStringName);
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                //Running query here
                using (SqlCommand myCommand = new SqlCommand(_sqlQuery, myCon))
                {
                    _myReader = myCommand.ExecuteReader();
                    _rowsAffected = _myReader.RecordsAffected;

                    _table.Load(_myReader);

                    _myReader.Close();
                    myCon.Close();
                }
            }
            if(_table.Rows.Count <= 0)
            {
                return new JsonResult(_rowsAffected + " Rows Affected");
            }
            return new JsonResult(_table);
        }

    }
}
