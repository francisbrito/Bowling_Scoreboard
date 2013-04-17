using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoreboard.Core.Tests
{
    [TestClass]
    public class NetworkShareFileReaderTests
    {
        //private NetworkShareFileReader _reader;

        //private const string NetworkShareHostName = "ANDROMEDA";
        //private const string TestFilesDirectory = "Users/Public/test_files";

        //[TestInitialize]
        //public void NetworkShareFileReaderTestsInitializer()
        //{
        //    _reader = new NetworkShareFileReader(NetworkShareHostName);

        //    var networkDirPath = GetFilePathFor(string.Empty); // Nope, I don't feel proud about this kind of hacks.

        //    if (!Directory.Exists(networkDirPath))
        //    {
        //        Directory.CreateDirectory(networkDirPath);
        //    }

        //    var netScorePath = GetFilePathFor("net_scores.txt");

        //    if (!File.Exists(netScorePath))
        //    {
        //        CreateTextFile(netScorePath);
        //    }

        //    var netNoAccessPath = GetFilePathFor("net_no_access.txt");

        //    if (!File.Exists(netNoAccessPath))
        //    {
        //        CreateNoAccessTextFile(netNoAccessPath);
        //    }
        //}

        #region Helpers
        private static void CreateNoAccessTextFile(string netNoAccessPath)
        {
            var fileInfo = new FileInfo(netNoAccessPath);

            fileInfo.CreateText().Close(); // Creates and closes the file so it can be accessed by other instructions.

            var accessControl = fileInfo.GetAccessControl();

            var denyRead = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    FileSystemRights.Read | FileSystemRights.ReadData,
                    AccessControlType.Deny);

            accessControl.SetAccessRule(denyRead);

            fileInfo.SetAccessControl(accessControl);
        }

        private static void CreateTextFile(string fileName)
        {
            File.CreateText(fileName).Close();
        } 
        #endregion

        //[TestMethod]
        //[ExpectedException(typeof(IOException))]
        //public void LoadFileShouldThrowAnExceptionIfConnectionToNetworkIsForcefullyClosed()
        //{
        //    var path = GetFilePathFor("net_scores.txt");

        //    _reader.LoadFile(path);
        //}

        //private static string GetFilePathFor(string fileName)
        //{
        //    var networkPath = string.Format(@"\\{0}", NetworkShareHostName);

        //    return Path.Combine(networkPath, TestFilesDirectory, fileName);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(UnauthorizedAccessException))]
        //public void LoadFileShouldThrowAnExceptionIfCantAccessNetworkFile()
        //{
        //    var path = GetFilePathFor("net_no_access.txt");

        //    _reader.LoadFile(path);
        //}

        //[TestMethod]
        //public void LoadFileShouldUsePathIfNetworkShareHostWasNotProvided()
        //{
        //    var path = GetFilePathFor("net_scores.txt");

        //    _reader = new NetworkShareFileReader();

        //    var hostNameWasNotSpecified = string.IsNullOrWhiteSpace(_reader.NetworkShareHostName);

        //    Assert.IsTrue(hostNameWasNotSpecified); // Make sure the host name was not set.

        //    try
        //    {
        //        _reader.LoadFile(path); // Attempt to load file.
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    Assert.IsTrue(_reader.FileReadStatus == FileReadStatus.Loaded); // Make sure the file was loaded.
        //}
    }
}
