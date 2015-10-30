using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace RESTShared.DAL
{
    public class PrefixDAO : DAOBase
    {
        public void CreateTable()
        {
            GetConnection(ConnectionString).CreateTable<Prefix>(overwrite: false);
        }
        public Prefix Insert(Prefix pre)
        {
            GetConnection(ConnectionString).Insert(pre);
            return pre;
        }
    }
}
