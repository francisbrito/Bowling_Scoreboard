using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class Throw
    {
        public Throw()
        {
            PinsDown = 0;
        }

        public Throw(string entry)
        {
            PinsDown = int.Parse(entry);
        }

        public int PinsDown { get; set; }

    }
}
