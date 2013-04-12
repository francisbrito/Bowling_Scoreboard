using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class LocalFileReader : IFileReader
    {
        public FileReadStatus FileReadStatus { get; private set; }
        public string LoadFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
