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

            var request = (HttpWebRequest)WebRequest.Create(uri);

            HttpWebResponse response = null;

            // NOTE:
            // This is a trick to get request/response kind of behavior working.
            // HttpWebRequest#GetResponse() throws an exception if the URL cannot be reached
            // instead of returning a 404!!
            // So the best course of action is to catch the exception an use it's Response property
            // to get the status code from the response.
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

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
