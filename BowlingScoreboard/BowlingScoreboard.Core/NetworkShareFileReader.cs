using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class NetworkShareFileReader : IFileReader
    {
        public NetworkShareFileReader(string networkShareHostName)
        {
            throw new NotImplementedException();
        }

        public FileReadStatus FileReadStatus { get; private set; }
        public string Text { get; private set; }

        public void LoadFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
