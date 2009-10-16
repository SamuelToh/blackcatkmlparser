using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    public class TestBlackCatKMLParserUI
    {
        private ILog log;
        private BlackCatParserUI parserUI;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        [SetUp]
        public void SetUp()
        {
            parserUI = new BlackCatParserUI();
        }

        //public bool folderIsWritable(String folderURL)
        [Test]
        public void TestFolderIsWritableTrue()
        {
            bool result = parserUI.folderIsWritable(@"..\..\Data");
            Assert.IsTrue(result);
        }

        //public bool folderExists(String folderURL)
        [Test]
        public void TestFolderExistsTrue()
        {
            bool result = parserUI.folderExists(@"..\..\Data");
            Assert.IsTrue(result);
        }

        [Test]
        public void TestFolderExistsFalse()
        {
            bool result = parserUI.folderExists(@"..\..\Data\SomeCrazyFolderName");
            Assert.IsFalse(result);
        }

        //public bool fileIsReadable(String fileURL)
        [Test]
        public void TestFileIsReadableTrue()
        {
            bool result = parserUI.fileIsReadable(@"..\..\Data\testMap1.mid");
            Assert.IsTrue(result);
        }

        //public bool fileExists(String fileURL)
        [Test]
        public void TestFileExistsTrue()
        {
            bool result = parserUI.fileExists(@"..\..\Data\testMap1.mid");
            Assert.IsTrue(result);
        }

        [Test]
        public void TestFileExistsFalse()
        {
            bool result = parserUI.fileExists(@"..\..\Data\nonexistant.mid");
            Assert.IsFalse(result);
        }

        //public bool hasSufficientDiskSpace(String drivePath)
        
        //public bool urlLengthIsValid(String fileURL)

        //public bool validationFileFormat(string filePath, string fileFormat)
        [Test]
        public void TestValidationFileFormatTrue()
        {
            Assert.IsTrue(parserUI.validationFileFormat("somefile.mif", BlackCatParserUI.FileFormat.MIF));
            Assert.IsTrue(parserUI.validationFileFormat("somefile.mid", BlackCatParserUI.FileFormat.MID));
            Assert.IsTrue(parserUI.validationFileFormat("somefile.kml", BlackCatParserUI.FileFormat.KML));
        }

        [Test]
        public void TestValidationFileFormatCaseInvariant()
        {
            Assert.IsTrue(parserUI.validationFileFormat("somefile.Mif", BlackCatParserUI.FileFormat.MIF));
            Assert.IsTrue(parserUI.validationFileFormat("somefile.mId", BlackCatParserUI.FileFormat.MID));
            Assert.IsTrue(parserUI.validationFileFormat("somefile.KML", BlackCatParserUI.FileFormat.KML));
        }

        [Test]
        public void TestValidationFileFormatFalse()
        {
            Assert.IsFalse(parserUI.validationFileFormat("somefile.kml", BlackCatParserUI.FileFormat.MIF));
            Assert.IsFalse(parserUI.validationFileFormat("somefile.exe", BlackCatParserUI.FileFormat.MID));
            Assert.IsFalse(parserUI.validationFileFormat("somefile.MIF", BlackCatParserUI.FileFormat.KML));
        }
    }
}
