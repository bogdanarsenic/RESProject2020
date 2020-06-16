﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualUI;
using VirtualUI.Access;
using VirtualUI.Controller;
using VirtualUI.Models;

namespace VirtualUITest.ControllerTest
{
    [TestFixture]
    public class DeltaControllerTest
    {
        private Delta delta;
        private Delta delta2;
        private const string FileId = "1";
        private DeltaController deltaController;
        private IDBManager database;

        [SetUp]
        public void SetUp()
        {

            database = FakeDBManager.Instance;

            Mock<Delta> deltaDouble = new Mock<Delta>();
            delta = deltaDouble.Object;

            Mock<Delta> deltaDouble2 = new Mock<Delta>();
            delta2 = deltaDouble2.Object;

            Mock<DeltaController> deltaControllerDouble = new Mock<DeltaController>();
            deltaController = deltaControllerDouble.Object;

        }

        [Test]

        public void AddDeltaGood()
        {
            delta.FileId = "nesto";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";

            deltaController.FakeAdd(delta);
            Assert.AreEqual(true, deltaController.FakeDeltaExists(delta.FileId));
        }


        [Test]
        public void AddDeltaBadNullArgument()
        {
            delta.FileId = null;
            delta.LineRange = "1,";
            delta.Content = "FakeContent";

            Assert.Throws<ArgumentNullException>(() =>
            {
                deltaController.FakeAdd(delta);
            }
            );
        }

        [Test]
        public void AddDeltaBadAboveLength()
        {
            delta.FileId = "opet";
            delta.LineRange = "gegegegegegegegegegegegegegegegegegegegegegegegegeg"; //51 karakter
            delta.Content = "FakeContent";
            Assert.Throws<ArgumentException>(() =>
            {
                deltaController.FakeAdd(delta);
            }
            );
        }

        [Test]
        public void AddDeltaBadEmptyString()
        {
            delta.FileId = "";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";
            deltaController.FakeAdd(delta);
            Assert.AreEqual(false, deltaController.FakeDeltaExists(delta.FileId));
        }

        [Test]
        public void AddDeltaBadTwoTimesSameDeltaAdd()
        {
            delta.FileId = "nesto";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";
            deltaController.FakeAdd(delta);
            Assert.AreEqual(false, deltaController.FakeAdd(delta));
        }

        [Test]
        public void UpdateDeltaGood()
        {
            delta.FileId = "nesto";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";
            deltaController.FakeAdd(delta);
  
            delta2.FileId = "nesto";
            delta2.LineRange = "1,2,3,";
            delta2.Content = "Fake";


            Assert.AreEqual(true, deltaController.FakeUpdateDelta(delta2));
            Assert.AreEqual("Fake", delta.Content);
        }

        [Test]
        public void UpdateDeltaBadNull()
        {
            delta.FileId = "nesto";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";
            database = new FakeDBManager();
            deltaController.FakeAdd(delta);

            delta2.FileId = null;
            delta2.LineRange = "1,2,3,";
            delta2.Content = "Fake";

            Assert.Throws<ArgumentNullException>(() =>
            {
                deltaController.FakeUpdateDelta(delta2);
            }
            );

            Assert.AreEqual("FakeContent", delta.Content);
        }

        [Test]
        public void UpdateDeltaBadNotSameFileId()
        {
            delta.FileId = "nesto";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";
            database = new FakeDBManager();
            deltaController.FakeAdd(delta);

            delta2.FileId = "nesto1";
            delta2.LineRange = "1,2,3,";
            delta2.Content = "Fake";


            Assert.AreEqual(false, deltaController.FakeUpdateDelta(delta2));
            Assert.AreEqual("FakeContent", delta.Content);
        }


        [Test]
        public void UpdateDeltaBadAboveLength()
        {
            delta.FileId = "nesto";
            delta.LineRange = "1,";
            delta.Content = "FakeContent";

            deltaController.FakeAdd(delta);

            delta2.FileId = "nesto1";
            delta2.LineRange = "gegegegegegegegegegegegegegegegegegegegegegegegegeg";
            delta2.Content = "Fake";


            Assert.Throws<ArgumentException>(() =>
            {
                deltaController.FakeUpdateDelta(delta2);
            }
            );
            Assert.AreEqual("FakeContent", delta.Content);
        }

        [Test]
        [TestCase("1")]
        public void DeltaExistsGood(string id)
        {
            delta.FileId = id;
            delta.LineRange = "1,";
            delta.Content = "Fake";
            deltaController.FakeAdd(delta);

            Assert.AreEqual(true, deltaController.FakeDeltaExists(id));
        }

        [Test]
        [TestCase("1")]
        public void DeltaExistsBad(string id)
        {
            delta.FileId = id;
            delta.LineRange = "1,";
            delta.Content = "Fake";

            Assert.AreEqual(false, deltaController.FakeDeltaExists(id));

        }


        [TearDown]
        public void TearDown()
        {
            delta = null;
        }



    }
}