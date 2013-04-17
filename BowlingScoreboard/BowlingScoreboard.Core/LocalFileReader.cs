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
                try
                {
                    Text = File.ReadAllText(path);
                    FileReadStatus = FileReadStatus.Loaded;
                }
                catch (UnauthorizedAccessException ex)
                {
                    var msg = string.Format("Unable to open file {0} due to its access rights.", path);

                    throw new UnauthorizedAccessException(msg, ex);
                }
            }
        }
    }
}
