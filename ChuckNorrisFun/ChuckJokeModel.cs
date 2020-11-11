using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorrisFun
{
    /*
    * Accepts json format
    * value - random jokes
    * total - number of jokes for with a specific word
    * Result - list of ID's - special values for GET that returns specific jokes from API
    */

    public class ChuckJokeModel
    {
        // attributes
        public string Value { get; set; }

        public int Total { get; set; }

        public ChuckWordResult[] Result { get; set; }

    }
}
