﻿using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;
namespace RESTShared
{
    [Route("/spellcheckrequest/{Word}/spellchecksuggestion","GET") ]
    [Serializable]
    public class SpellCheckSuggestion
    {
        public string Word;
        public int Rank;
    }
}
