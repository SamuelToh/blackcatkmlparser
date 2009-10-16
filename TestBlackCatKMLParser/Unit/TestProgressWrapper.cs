using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Mocks;
using BlackCat;
using log4net;
using log4net.Config;

namespace TestBlackCatKMLParser
{
    public class TestProgressWrapper
    {
        private ILog log;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            BasicConfigurator.Configure();
            log = LogManager.GetLogger(this.ToString());
        }

        //public void SetPercentage(int percentage)
        [Test]
        public void TestSetPercentage0()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            wrapper.SetPercentage(0);
            Assert.AreEqual(0, wrapper.GetPercentage());
        }

        [Test]
        public void TestSetPercentage55()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            wrapper.SetPercentage(55);
            Assert.AreEqual(55, wrapper.GetPercentage());
        }

        [Test]
        public void TestSetPercentage100()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            wrapper.SetPercentage(100);
            Assert.AreEqual(100, wrapper.GetPercentage());
        }

        //public int GetPercentage()
        [Test]
        public void TestGetPercentage0()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            bar.Value = 0;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            Assert.AreEqual(0, wrapper.GetPercentage());
        }

        [Test]
        public void TestGetPercentage25()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            bar.Value = 25;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            Assert.AreEqual(25, wrapper.GetPercentage());
        }

        [Test]
        public void TestGetPercentage50()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            bar.Value = 50;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            Assert.AreEqual(50, wrapper.GetPercentage());
        }

        [Test]
        public void TestGetPercentage75()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            bar.Value = 75;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            Assert.AreEqual(75, wrapper.GetPercentage());
        }

        [Test]
        public void TestGetPercentage100()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            bar.Value = 100;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            Assert.AreEqual(100, wrapper.GetPercentage());
        }
        
        //public void Increment(int value)
        [Test]
        public void TestIncrement()
        {
            ProgressBar bar = new ProgressBar();
            bar.Maximum = 100;
            bar.Minimum = 0;
            bar.Value = 0;
            ProgressWrapper wrapper = new ProgressWrapper(bar);
            wrapper.Increment(10);
            Assert.AreEqual(10, wrapper.GetPercentage());
            wrapper.Increment(10);
            Assert.AreEqual(20, wrapper.GetPercentage());
            wrapper.Increment(10);
            Assert.AreEqual(30, wrapper.GetPercentage());
        }

    }
}
