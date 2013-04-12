using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public interface IFileReader
    {
        FileReadStatus FileReadStatus { get; }
        string LoadFile(string path);
    }
}
