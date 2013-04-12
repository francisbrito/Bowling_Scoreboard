using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class NetworkShareFileReader : IFileReader
    {
        private string _networkShareHostName;

        public NetworkShareFileReader(string networkShareHostNameName)
        {
            _networkShareHostName = networkShareHostNameName;

            FileReadStatus = FileReadStatus.NotLoaded;
            Text = string.Empty;
        }

        public  string NetworkShareHostName
        {
            get { return _networkShareHostName; }
            set { _networkShareHostName = value; }
        }
        public FileReadStatus FileReadStatus { get; private set; }
        public string Text { get; private set; }

        public void LoadFile(string path)
        {
            var uncPath = string.Format(@"\\{0}{1}", _networkShareHostName, path);

            if (!File.Exists(uncPath))
            {
                FileReadStatus = FileReadStatus.NotFound;
            }
            else
            {
                try
                {
                    Text = File.ReadAllText(uncPath);

                    FileReadStatus = FileReadStatus.Loaded;
                }
                // May happen if the connection is forcefully closed.
                catch (IOException)
                {
                    FileReadStatus = FileReadStatus.NotLoaded;
                }
            }
        }
    }
}
