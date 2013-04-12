using System.IO;
using BowlingScoreboard.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using System.Net;

namespace BowlingScoreboard.Specs
{
    [Binding]
    public class ReadATextFileSteps
    {
        private string _fileName;
        private string _filePath;

        private IFileReader _fileReader;
        private string _networkShareHostName;
        private string _internetServerName;

        #region Setup & Clean up
        [BeforeTestRun]
        public static void SetupTestFiles()
        {
            #region For local FS

            Directory.CreateDirectory("./test_files");

            File.CreateText("./test_files/scores.txt").Close();
            #endregion
            #region For network share

            var dirPath = Path.Combine(@"//ANDROMEDA", "Users/Public/test_files");
            Directory.CreateDirectory(dirPath);

            var filePath = Path.Combine(dirPath, "net_scores.txt");
            File.CreateText(filePath).Close();

            #endregion
        }

        [AfterTestRun]
        public static void CleanUpTestFiles()
        {
            #region For local FS

            File.Delete("./test_files/scores.txt");
            Directory.Delete("./test_files");

            #endregion

            #region For network share

            var dirPath = Path.Combine(@"\\ANDROMEDA", "Users/Public/test_files");
            var filePath = Path.Combine(dirPath, "net_scores.txt");

            File.Delete(filePath);
            Directory.Delete(dirPath);

            #endregion
        }
        #endregion

        [Given(@"I want to load a file called ""(.*)""")]
        public void GivenIWantToLoadAFileCalled(string fileName)
        {
            _fileName = fileName;
        }

        [Given(@"I think its located at the path ""(.*)""")]
        public void GivenIThinkItsLocatedAtThePath(string filePath)
        {
            _filePath = filePath;
        }

        [Given(@"it exists in my local file system")]
        public void GivenItExistsInMyLocalFileSystem()
        {
            // NOTE:
            // I'm running two implicit steps here.
            // I tell the application I'll be reading from the local file system.
            // AND I tell the application the file exists.
            // This should be seperated into two steps just like other scenarios.
            _fileReader = new LocalFileReader();

            var fullPath = string.Format("{0}{1}", _filePath, _fileName);

            Assert.IsTrue(File.Exists(fullPath));
        }

        [When(@"I press load")]
        public void WhenIPressLoad()
        {
            var fullPath = string.Format("{0}{1}", _filePath, _fileName);

            _fileReader.LoadFile(fullPath);
        }

        [Then(@"the application should load the file")]
        public void ThenTheApplicationShouldLoadTheFile()
        {
            Assert.IsTrue(_fileReader.FileReadStatus == FileReadStatus.Loaded);
        }

        [Given(@"it doesnt exists in my local file system")]
        public void GivenItDoesntExistsInMyLocalFileSystem()
        {
            _fileReader = new LocalFileReader();

            var fullPath = string.Format("{0}{1}", _filePath, _fileName);

            Assert.IsFalse(File.Exists(fullPath));
        }

        [Then(@"the application should notify me the file doesnt exists")]
        public void ThenTheApplicationShouldNotifyMeTheFileDoesntExists()
        {
            Assert.IsTrue(_fileReader.FileReadStatus == FileReadStatus.NotFound);
        }

        [Given(@"its at a network share host called ""(.*)""")]
        public void GivenItsAtANetworkShareHostCalled(string networkShareHostName)
        {
            _networkShareHostName = networkShareHostName;

            _fileReader = new NetworkShareFileReader(networkShareHostName);
        }

        [Given(@"it exists at the network share host")]
        public void GivenItExistsAtTheNetworkShareHost()
        {
            var fullPath = string.Format("//{0}{1}{2}", _networkShareHostName, _filePath, _fileName);

            Assert.IsTrue(File.Exists(fullPath));
        }

        [Given(@"it doesnt exists at the network share host")]
        public void GivenItDoesntExistsAtTheNetworkShareHost()
        {
            var fullPath = string.Format("//{0}{1}{2}", _networkShareHostName, _filePath, _fileName);

            Assert.IsFalse(File.Exists(fullPath));
        }

        [Given(@"its at an internet server called ""(.*)""")]
        public void GivenItsAtAnInternetServerCalled(string internetServerName)
        {
            _internetServerName = internetServerName;

            _fileReader = new InternetFileReader(internetServerName);
        }

        [Given(@"it exists at the internet server")]
        public void GivenItExistsAtTheInternetServer()
        {
            var uri = string.Format("http://{0}/{1}/{2}", _internetServerName, _filePath, _fileName);

            HttpWebResponse response = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Assert.Fail(ex.Message, ex.Response);
            }

            // It should be a 200 (OK)
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Given(@"it doesnt exists at the internet server")]
        public void GivenItDoesntExistsAtTheInternetServer()
        {
            var uri = string.Format("http://{0}/{1}/{2}", _internetServerName, _filePath, _fileName);

            HttpWebResponse response = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                //Assert.Fail(ex.Message, ex.Response);
                response = (HttpWebResponse)ex.Response;
            }

            // It should be a 404 (Not Found)
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

    }
}
