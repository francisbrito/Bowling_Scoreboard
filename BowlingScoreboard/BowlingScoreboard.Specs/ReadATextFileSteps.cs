using System;
using TechTalk.SpecFlow;

namespace BowlingScoreboard.Specs
{
    [Binding]
    public class ReadATextFileSteps
    {
        private string _fileName;
        private string _directoryName;
        private string _serverName;
        private string _fileShareName;

        [Given(@"I want to load a file called ""(.*)""")]
        public void GivenIWantToLoadAFileCalled(string fileName)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I think its located at the directory ""(.*)"" in my local file system")]
        public void GivenIThinkItsLocatedAtTheDirectoryInMyLocalFileSystem(string directoryName)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"it exists")]
        public void GivenItExists()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"it doesnt exists")]
        public void GivenItDoesntExists()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I think its located at the directory ""(.*)"" in a network share host called ""(.*)""")]
        public void GivenIThinkItsLocatedAtTheDirectoryInANetworkShareHostCalled(string directioryName, string fileShareName)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I think its located at the directory ""(.*)"" in a  internet server called ""(.*)""")]
        public void GivenIThinkItsLocatedAtTheDirectoryInAInternetServerCalled(string directoryName, string serverName)
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
        
        [Then(@"the application should notify me the file doesnt exists")]
        public void ThenTheApplicationShouldNotifyMeTheFileDoesntExists()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
