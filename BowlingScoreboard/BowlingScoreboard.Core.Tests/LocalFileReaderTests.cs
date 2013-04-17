using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoreboard.Core.Tests
{
    [TestClass]
    public class LocalFileReaderTests
    {
        private LocalFileReader _reader;
        private const string TestFilesDirectory = "./test_files";

        [TestInitialize]
        public void LocalFileReaderTestsInitializer()
        {
            _reader = new LocalFileReader();

            if (!Directory.Exists(TestFilesDirectory))
            {
                Directory.CreateDirectory(TestFilesDirectory);
            }

            string noAccessFilePath = GetTestFilePathFor("no_access.txt");

            if (!File.Exists(noAccessFilePath))
            {
                CreateNoAccessFile(noAccessFilePath);
            }

            string scoresFilePath = GetTestFilePathFor("scores.txt");

            if (!File.Exists(scoresFilePath))
            {
                CreateScoresFile(scoresFilePath);
            }
        }

        #region Helpers

        private static void CreateScoresFile(string scoresFilePath)
        {
            File.CreateText(scoresFilePath).Close();
        }

        private static void CreateNoAccessFile(string noAccessFilePath)
        {
            var fileInfo = new FileInfo(noAccessFilePath);

            fileInfo.CreateText().Close(); // Creates and closes the file so it can be accessed by other instructions.

            var accessControl = fileInfo.GetAccessControl();

            var denyRead = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    FileSystemRights.Read | FileSystemRights.ReadData,
                    AccessControlType.Deny);

            accessControl.SetAccessRule(denyRead);

            fileInfo.SetAccessControl(accessControl);
        }

        private static string GetTestFilePathFor(string fileName)
        {
            return Path.Combine(TestFilesDirectory, fileName);
        } 
        #endregion

        [TestMethod]
        [ExpectedException(typeof (UnauthorizedAccessException))]
        public void LoadFileShouldThrowAnExceptionIfCantAccessFile()
        {
            var path = GetTestFilePathFor("no_access.txt");

            _reader.LoadFile(path);
        }

        [TestMethod]
        public void LoadFileShouldLoadFileIfItExists()
        {
            var path = GetTestFilePathFor("scores.txt");

            _reader.LoadFile(path);

            Assert.IsTrue(_reader.FileReadStatus == FileReadStatus.Loaded);
        }

        [TestMethod]
        public void FileReadStatusShouldDefaultToNotLoaded()
        {
            Assert.IsTrue(_reader.FileReadStatus == FileReadStatus.NotLoaded);
        }
    }
}
