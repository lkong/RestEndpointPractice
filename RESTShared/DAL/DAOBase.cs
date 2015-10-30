using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Configuration;
using System.Data;

namespace RESTShared.DAL
{
    public class DAOBase : DbFactoryService
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                //return @"SERVER=127.0.0.1;Port=5349;User Id=postgres;Password=password;Database=arsite_web_development;"; 
                return ConfigurationManager.AppSettings["DBConnectionString"];
            }
        }
        private static IDbConnection connection=null;
        protected  IDbConnection GetConnection(string connectionString)
        {
            if (connection==null)
                connection=this.GetFactory(SqliteDialect.Provider, connectionString).Open();
            return connection;
        } 
    }
}
