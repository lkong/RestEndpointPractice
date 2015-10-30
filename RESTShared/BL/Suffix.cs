using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace RESTShared
{
    [Alias("suffixes")]
    public class Suffix
    {
        [AutoIncrement]
        [Alias("id")]
        public int Id { get; set; }

        [Alias("spell")]
        public string Spell { get; set; }

        [References(typeof(Word))]
        public int FullWordId { get; set; } 
    }
}
