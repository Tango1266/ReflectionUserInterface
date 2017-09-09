using AutoUIConsole.Components.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SelectionTests
    {
        [TestMethod]
        public void GetNextPathLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "lev2";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("leaf", path.nextLevel);
        }

        [TestMethod]
        public void GetBasePathLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "lev2";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("lev2", path.baseLevel);
        }

        [TestMethod]
        public void GetPreviousPathLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "lev2";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("lev1", path.previousLevel);
        }

    }
}
