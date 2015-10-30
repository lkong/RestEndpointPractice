using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTShared
{
    public abstract class DbFactoryService
    {
        private OrmLiteConnectionFactory _ormLiteConnectionFactory = null;

        /// <summary>
        /// Get database connection factory for opening a pooled connection.
        /// </summary>
        /// <param name="dialectProvider"><see cref="IOrmLiteDialectProvider"/>Database Dialect Provider.</param>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns><see cref="OrmLiteConnectionFactory"/>.</returns>
        public virtual OrmLiteConnectionFactory GetFactory(IOrmLiteDialectProvider dialectProvider, string connectionString)
        {
            try
            {

                if (_ormLiteConnectionFactory == null)
                {
                    _ormLiteConnectionFactory = new OrmLiteConnectionFactory(
                        connectionString,SqliteOrmLiteDialectProvider.Instance);
                }
            }

            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return _ormLiteConnectionFactory;
        }
    }
}
