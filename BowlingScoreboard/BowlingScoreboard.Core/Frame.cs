using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class Frame
    {
        public Frame()
        {
            Throws = new Throw[]
                {
                    new Throw(),
                    new Throw(),
                    new Throw(), 
                };
        }

        public Throw[] Throws { get; set; }

        public int Score { get; set; }

        public FrameType Type { get; set; }
    }

    public enum FrameType
    {
        Normal,
        Spare,
        Strike
    }
}
