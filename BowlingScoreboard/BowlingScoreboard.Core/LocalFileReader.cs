using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class LocalFileReader : IFileReader
    {
        public LocalFileReader()
        {
            FileReadStatus = FileReadStatus.NotLoaded;
            Text = string.Empty;
        }

        public FileReadStatus FileReadStatus { get; private set; }
        public string Text { get; private set; }

        public void LoadFile(string path)
        {
            if (!File.Exists(path))
            {
                FileReadStatus = FileReadStatus.NotFound;
            }
            else
            {
                Text = File.ReadAllText(path);

                FileReadStatus = FileReadStatus.Loaded;
            }
        }
    }
}
