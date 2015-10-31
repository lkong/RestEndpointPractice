using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace RESTShared.DAL
{
    public class CommonMisspellDAO : DAOBase
    {
        public void CreateTable()
        {
            GetConnection(ConnectionString).CreateTable<CommonMisspell>(overwrite: false);
        }
        public CommonMisspell Insert(CommonMisspell misspell)
        {
            GetConnection(ConnectionString).Save(misspell);
            return misspell;
        }
        public CommonMisspell Query(CommonMisspell misspell)
        {
            GetConnection(ConnectionString).Single<CommonMisspell>(w=>w.Spell.ToLower()==misspell.Spell.ToLower());
            return misspell; 
        }
        public CommonMisspell Query(string word)
        {
            CommonMisspell misspell=GetConnection(ConnectionString).Single<CommonMisspell>(cm => cm.Spell.ToLower() == word.ToLower());
            return misspell;
        }
        public string QueryCorrectSpell(string word)
        {
            CommonMisspell misspell = GetConnection(ConnectionString).Single<CommonMisspell>(cm => cm.Spell.ToLower() == word.ToLower());
            string spell = GetConnection(ConnectionString).Single<Word>(w=>w.Id==misspell.FullWordId).Spell;
            return spell;
        }
    }
}
