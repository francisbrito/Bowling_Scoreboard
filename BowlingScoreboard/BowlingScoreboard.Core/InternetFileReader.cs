using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class InternetFileReader : IFileReader
    {
        private string _internetServerName;

        public InternetFileReader(string internetServerName)
        {
            _internetServerName = internetServerName;

            FileReadStatus = FileReadStatus.NotLoaded;
        }

        public string InternetServerName
        {
            get { return _internetServerName; }
            set { _internetServerName = value; }
        }

        public FileReadStatus FileReadStatus { get; private set; }
        public string Text { get; private set; }

        public void LoadFile(string path)
        {
            var uri = string.Format("http://{0}/{1}", _internetServerName, path);

            var request = (HttpWebRequest) WebRequest.Create(uri);
            var response = (HttpWebResponse) request.GetResponse();

            var httpStatusCode = response.StatusCode;

            if (httpStatusCode == HttpStatusCode.NotFound)
            {
                FileReadStatus = FileReadStatus.NotFound;
            }
            else
            {
                try
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        Text = reader.ReadToEnd();

                        FileReadStatus = FileReadStatus.Loaded;
                    }
                }
                catch (ArgumentNullException)
                {
                    // This may happen if the connection is forcefully closed. 
                    FileReadStatus = FileReadStatus.NotLoaded;
                }
            }
        }
    }
}
