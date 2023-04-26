using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Databases
{
    public class Sql_LiteDatabaseAccess : IDataBaseAccess
    {
        private readonly IConfiguration _config;

        public Sql_LiteDatabaseAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName, bool isStoredProc = false)
        {
            //Implement SQL Lite SQL calls through Dapper
            throw new NotImplementedException();
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoredProc = false)
        {
            //Implement SQL Lite SQL calls through Dapper
            throw new NotImplementedException();
        }
    }
}
