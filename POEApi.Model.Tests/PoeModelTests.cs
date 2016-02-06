﻿using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using POEApi.Transport;

namespace POEApi.Model.Tests
{
    [TestClass]
    public class PoeModelTests
    {
        private Mock<ITransport> _mockTransport;
        private POEModel _model;

        [TestInitialize]
        public void TestSetup()
        {
            _mockTransport = new Mock<ITransport>();
            _model = new POEModel { Transport = _mockTransport.Object };
        }

        [TestMethod]
        public void GetStashTest()
        {
            //Nasty little gotcha See: http://blogs.msdn.com/b/cie/archive/2014/03/19/encountered-unexpected-character-239-error-serializing-json.aspx
            //You have to remove the first three bytes from the file.
            string fakeStashInfo = Encoding.UTF8.GetString(Files.SampleStash);
            using (var stream = GenerateStreamFromString(fakeStashInfo))
            {
                _mockTransport.Setup(m => m.GetStash(0, "", "", false)).Returns(stream);

                var stash = _model.GetStash(0, "", "");

                Assert.IsNotNull(stash);

                Assert.AreEqual(stash.Tabs.Count, 17);
            }
        }

        [TestMethod]
        public void GetAccountNameTest()
        {
            var fakeAccountNameResponse = "{\"accountName\":\"fakeAccountName\"}";

            using (var stream = GenerateStreamFromString(fakeAccountNameResponse))
            {
                _mockTransport.Setup(m => m.GetAccountName()).Returns(stream);

                var account = _model.GetAccountName();

                Assert.AreEqual(account, "fakeAccountName");
            }
        }

        public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
