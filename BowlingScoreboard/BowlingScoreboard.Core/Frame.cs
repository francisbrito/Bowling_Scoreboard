using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class Frame
    {
        public Throw[] Throws { get; set; }

        public int Score { get; set; }

        public object Type
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    public enum FrameType
    {
        Normal,
        Spare,
        Strike
    }
}
