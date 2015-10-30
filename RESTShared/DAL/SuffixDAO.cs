using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace RESTShared.DAL
{
    public class SuffixDAO : DAOBase
    {
        public void CreateTable()
        {
            GetConnection(ConnectionString).CreateTable<Suffix>(overwrite: false);
        }
        public Suffix Insert(Suffix suf)
        {
            GetConnection(ConnectionString).Insert(suf);
            return suf;
        }
    }
}
