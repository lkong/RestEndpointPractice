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
            GetConnection(ConnectionString).Save(word);
            return word;
        }
        public Word Query(Word word)
        {
            word=GetConnection(ConnectionString).Single<Word>(w=>w.Spell.ToLower()==word.Spell.ToLower());
            return word; 
        }
        public List<Word> FindPartialMatch(string word)
        {
            List<Word> matched=GetConnection(ConnectionString).Select<Word>("spell LIKE '%"+word+"%'");
            return matched; 
        }
    }
}
