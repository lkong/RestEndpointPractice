using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTShared
{
    [Route("/spellcheckrequest") ]
    [Route("/spellcheckrequest/{Word}") ]
    public class SpellCheckRequest
    {
        public string Word;
    }
}
