﻿using RESTShared;
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
        public bool DoesWordExist(Word word)
        {
            new WordDAO().Query(word); ;
            if (word.Id == 0)
                return false;
            else
                return true;
        
        }
        public bool IsACommonMisspell(Word word)
        {
            CommonMisspell cm = new CommonMisspellDAO().Query(word.Spell);
            if (cm.Id == 0)
                return false;
            else
                return true;

        }
        public List<SpellCheckSuggestion> GetSpellSuggestions(Word word)
        {
            List<SpellCheckSuggestion> suggestionList = new List<SpellCheckSuggestion>();
            Dictionary<string, int> suggestionDict = new Dictionary<string, int>();
            GetSpellSuggestions(word.Spell, 0,suggestionDict);
            foreach (string k in suggestionDict.Keys)
            {
                suggestionList.Add(new SpellCheckSuggestion(){Word= k,Rank= suggestionDict[k]});
            }
            return suggestionList;
        
        }
        public void GetSpellSuggestions(string word, int LevenshteinDistance, Dictionary<string, int> suggestionDict)
        {
            if (word.Length<3)
                return;
            List<Word> partialMatch=new WordDAO().FindPartialMatch(word);
            foreach (Word w in partialMatch)
                if (!suggestionDict.Keys.Contains(w.Spell))
                    suggestionDict[w.Spell]=LevenshteinDistance;
            GetSpellSuggestions(word.Substring(1), LevenshteinDistance + 1, suggestionDict);
            GetSpellSuggestions(word.Substring(0, word.Length - 1), LevenshteinDistance + 1, suggestionDict);
        }
        private void LoadData()
        {
            new WordDAO().CreateTable();
            new CommonMisspellDAO().CreateTable();
            Word apple = new Word() { Spell = "Apple" };
            new WordDAO().Insert(apple);
            new CommonMisspellDAO().Insert(new CommonMisspell() { FullWordId = apple.Id, Spell="epple" });
            new CommonMisspellDAO().Insert(new CommonMisspell() { FullWordId = apple.Id, Spell="appl" });
            new CommonMisspellDAO().Insert(new CommonMisspell() { FullWordId = apple.Id, Spell="appla" });
            //List<SpellCheckSuggestion> suggestionsList = GetSpellSuggestions(new Word() { Spell = "Zpple" });

        }
    }
}