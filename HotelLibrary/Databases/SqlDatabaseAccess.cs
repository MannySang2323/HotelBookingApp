using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

namespace HotelLibrary.Databases
{
    public class SqlDataBaseAccess : IDataBaseAccess
    {
        private readonly IConfiguration _config;

        public SqlDataBaseAccess(IConfiguration config)
        {
            _config = config;
        }


        /// <summary>
        /// Generic Dapper Load Data
        /// </summary>
        /// <typeparam name="T"> Model to use </typeparam>
        /// <typeparam name="U"> Parameters to use </typeparam>
        /// <param name="sqlStatement"> SQL statement to execute </param>
        /// <param name="parameters"> Parameters to pass in </param>
        /// <param name="connectionStringName"> Connection string Name from appsettings.json </param>
        /// <param name="isStoredProcedure"> Bool if we are usinhg stored procs </param>
        /// <returns> Populated model </returns>
        public List<T> LoadData<T, U>(string sqlStatement, U parameters,
                                    string connectionStringName, bool isStoredProc = false)
        {
            //Get full conn string using _config
            string connectionString = _config.GetConnectionString(connectionStringName);

            //Statement or SP?
            CommandType commandTypeToUse = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

            using IDbConnection conn = new SqlConnection(connectionString);

            List<T> rows = conn.Query<T>(sqlStatement, parameters, commandType: commandTypeToUse).ToList();
            
            return rows;
        }


        /// <summary>
        /// Generic Dapper Execute
        /// </summary>
        /// <typeparam name="T"> Parameters to use  </typeparam>
        /// <param name="sqlStatement"> SQL statement to execute </param>
        /// <param name="parameters"> Parameters to pass in </param>
        /// <param name="connectionStringName"> Connection string Name from appsettings.json </param>
        /// <param name="isStoredProcedure"> Bool if we are usinhg stored procs </param>
        public void SaveData<T>(string sqlStatement, T parameters,
                                string connectionStringName, bool isStoredProc = false)
        {
            //Get full conn string using _config
            string connectionString = _config.GetConnectionString(connectionStringName);

            //Statement or SP?
            CommandType commandTypeToUse = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

            using IDbConnection conn = new SqlConnection(connectionString);

            conn.Execute(sqlStatement, parameters, commandType: commandTypeToUse);
        }

    }
}





