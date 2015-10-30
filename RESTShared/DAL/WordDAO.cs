using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using System.Data;

namespace RESTShared.DAL
{
    public class WordDAO : DAOBase
    {
        public void CreateTable()
        {
            GetConnection(ConnectionString).CreateTable<Word>(overwrite: false);
        }
        public Word Insert(Word word)
        {
            GetConnection(ConnectionString).Insert(word);
            return word;
        }
    }
}
