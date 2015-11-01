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
        /// <summary>
        /// Check if the word is spelled correctly
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool DoesWordExist(Word word)
        {
            if (word.Spell==null || word==null)
                return false;
            
            word = new WordDAO().Query(word);
            if (word == null || word.Id == 0)
                return false;
            else
                return true;
        
        }
        /// <summary>
        /// Decide if given word is a common misspell
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsACommonMisspell(Word word)
        {
            CommonMisspell cm = new CommonMisspellDAO().Query(word.Spell);
            if (cm==null || cm.Id == 0)
                return false;
            else
                return true;

        }
        /// <summary>
        /// Insert a word into the DB
        /// </summary>
        /// <param name="word"></param>
        public void InsertWord(Word word)
        {
            new WordDAO().Insert(word);
        }
        /// <summary>
        /// Look for common misspell
        /// </summary>
        /// <param name="misspell"></param>
        /// <returns></returns>
        public string GetCorrectSpellForACommonMisspell(string misspell)
        {
            return new CommonMisspellDAO().QueryCorrectSpell(misspell);
        }
        /// <summary>
        /// Look up spell suggestion for a given word
        /// Look for the exact match first
        /// Then look for common misspells
        /// Then look up recursively for partial match
        /// </summary>
        /// <param name="word"></param>
        /// <returns> return a list of spell suggestion with rank</returns>
        public List<SpellCheckSuggestion> GetSpellSuggestions(Word word)
        {
            if (word.Spell==null || word.Spell.Length<3)
                return new List<SpellCheckSuggestion>();
            if (DoesWordExist(word))
                return new List<SpellCheckSuggestion>() { new SpellCheckSuggestion() {Word="Correct spell", Rank=-1 } };
            List<SpellCheckSuggestion> suggestionList = new List<SpellCheckSuggestion>();
            Dictionary<string, int> suggestionDict = new Dictionary<string, int>();
            if (IsACommonMisspell(word))
                suggestionDict.Add(GetCorrectSpellForACommonMisspell(word.Spell), 0);
            GetSpellSuggestions(word.Spell, 0,suggestionDict);
            foreach (string k in suggestionDict.Keys)
            {
                suggestionList.Add(new SpellCheckSuggestion(){Word= k,Rank= suggestionDict[k]});
            }
            return suggestionList;
        
        }
        /// <summary>
        /// Recursively checking for partial fit of given word
        /// Add result and rank (levenshtein distance) to a given dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <param name="LevenshteinDistance"></param>
        /// <param name="suggestionDict"></param>
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
        /// <summary>
        /// Init DB
        /// </summary>
        private void LoadData()
        {
            new WordDAO().CreateTable();
            new CommonMisspellDAO().CreateTable();
            Word apple = new Word() { Spell = "Apple" };
            Word people = new Word() { Spell = "People" };

            apple=new WordDAO().Insert(apple);
            people=new WordDAO().Insert(people);
            new CommonMisspellDAO().Insert(new CommonMisspell() { FullWordId = apple.Id, Spell="appla" });
            //List<SpellCheckSuggestion> suggestionsList = GetSpellSuggestions(new Word() { Spell = "Zpple" });

        }
    }
}