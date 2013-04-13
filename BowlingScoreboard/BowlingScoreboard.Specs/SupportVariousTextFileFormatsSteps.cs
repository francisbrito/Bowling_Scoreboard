using System;
using System.IO;
using BowlingScoreboard.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BowlingScoreboard.Specs
{
    [Binding]
    public class SupportVariousTextFileFormatsSteps
    {
        private IFileFormatValidator _formatValidator;
        private string _text;
        const string TestDirPath = "./test_files";
        const string TestFileName = "scores-1c2spp.txt";

        [BeforeTestRun]
        public static void SetupTest()
        {
            

            Directory.CreateDirectory(TestDirPath);

            var filePath = Path.Combine(TestDirPath, TestFileName);

            // One-Column-Two-Shots-Per-Player format
            using (var fileWriter = File.CreateText(filePath))
            {
                for (int i = 0; i < 42; i++)
                {
                    fileWriter.WriteLine(10);
                }
            }
        }

        [AfterTestRun]
        public static void CleanUp()
        {
            var filePath = Path.Combine(TestDirPath, TestFileName);

            File.Delete(filePath);
            Directory.Delete(TestDirPath);
        }

        [Given(@"it has one column two shots per player at a time format")]
        public void GivenItHasOneColumnTwoShotsPerPlayerAtATimeFormat()
        {
            _formatValidator = new DefaultFileFormatValidator();
        }

        [When(@"it loads")]
        public void WhenItLoads()
        {
            var reader = new LocalFileReader();

            var fullPath = Path.Combine(TestDirPath, TestFileName);

            reader.LoadFile(fullPath);

            _text = reader.Text;
        }
        
        [Then(@"the application should accept the file")]
        public void ThenTheApplicationShouldAcceptTheFile()
        {
            var isValid = _formatValidator.IsValid(_text);

            Assert.IsTrue(isValid);
        }
    }
}
