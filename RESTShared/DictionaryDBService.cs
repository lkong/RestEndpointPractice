using RESTShared;
using RESTShared.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RESTShared
{
    public class DictionaryDBService
    {
        public void Init()
        {
            LoadData();
            
        }
        private void LoadData()
        {
            new WordDAO().CreateTable();
            new SuffixDAO().CreateTable();
            new PrefixDAO().CreateTable();
            Word apple = new Word() { Spell = "Apple" };
            new WordDAO().Insert(apple);
            new SuffixDAO().Insert(new Suffix() { FullWordId = apple.Id, Spell="ple" });
            new SuffixDAO().Insert(new Suffix() { FullWordId = apple.Id, Spell="le" });
            new SuffixDAO().Insert(new Suffix() { FullWordId = apple.Id, Spell="pple" });
            new PrefixDAO().Insert(new Prefix() { FullWordId = apple.Id, Spell="ap" });
            new PrefixDAO().Insert(new Prefix() { FullWordId = apple.Id, Spell="app" });
            new PrefixDAO().Insert(new Prefix() { FullWordId = apple.Id, Spell="appl" });
        }
    }
}