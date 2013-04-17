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

        public NetworkShareFileReader()
            : this(string.Empty)
        {
        }

        public string NetworkShareHostName
        {
            get { return _networkShareHostName; }
            set { _networkShareHostName = value; }
        }

        public FileReadStatus FileReadStatus { get; private set; }
        public string Text { get; private set; }

        public void LoadFile(string path)
        {
            var uncPath = string.Format(@"\\{0}{1}", _networkShareHostName, path);

            if (string.IsNullOrWhiteSpace(_networkShareHostName))
            {

            }

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
                catch (UnauthorizedAccessException ex)
                {
                    var msg = string.Format("Unable to load file {0} due to its access rights.", uncPath);
                    throw new UnauthorizedAccessException(msg, ex);
                }
                catch (IOException ex)
                {
                    FileReadStatus = FileReadStatus.NotLoaded;
                }
            }
        }
    }
}
