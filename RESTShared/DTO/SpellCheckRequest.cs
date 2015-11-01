using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTShared
{
    public class SpellCheckRequest : IReturn<SpellCheckSuggestion>
    {
        public string Word { get; set; }
    }
}
