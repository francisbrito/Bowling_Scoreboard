using System;
using TechTalk.SpecFlow;

namespace BowlingScoreboard.Specs
{
    [Binding]
    public class ReadATextFileSteps
    {
        private string _fileName;
        private string _filePath;
        private string _networkShareHostName;
        private string _internetServerName;
        [Given(@"I want to load a file called ""(.*)""")]
        public void GivenIWantToLoadAFileCalled(string fileName)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I think its located at the path ""(.*)""")]
        public void GivenIThinkItsLocatedAtThePath(string filePath)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"it exists in my local file system")]
        public void GivenItExistsInMyLocalFileSystem()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press load")]
        public void WhenIPressLoad()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the application should load the file")]
        public void ThenTheApplicationShouldLoadTheFile()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"it doesnt exists in my local file system")]
        public void GivenItDoesntExistsInMyLocalFileSystem()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the application should notify me the file doesnt exists")]
        public void ThenTheApplicationShouldNotifyMeTheFileDoesntExists()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"its at a network share host called ""(.*)""")]
        public void GivenItsAtANetworkShareHostCalled(string networkShareHostName)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"it exists at the network share host")]
        public void GivenItExistsAtTheNetworkShareHost()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"it doesnt exists at the network share host")]
        public void GivenItDoesntExistsAtTheNetworkShareHost()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"its at an internet server called ""(.*)""")]
        public void GivenItsAtAnInternetServerCalled(string internetServerName)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"it exists at the internet server")]
        public void GivenItExistsAtTheInternetServer()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"it doesnt exists at the internet server")]
        public void GivenItDoesntExistsAtTheInternetServer()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
