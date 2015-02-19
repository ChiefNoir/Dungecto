using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dungecto.UI;
using System.Windows;
using System.Windows.Controls;

namespace Tests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void AddTest()
        {
            var map = new MapCanvas();

            map.Add(null);
            Assert.AreEqual(0, map.Children.Count,"null add failed");


            var tile0 = new MapTile();
            map.Add(tile0);
            Assert.AreEqual(1, map.Children.Count, "Add failed");
            Assert.AreEqual(true, map.Children.Contains(tile0), "Add failed");


            map.Add(tile0);            
            Assert.AreEqual(1, map.Children.Count, "Duplicate add failed");


            var tile2 = new MapTile();
            map.Add(tile2);
            Assert.AreEqual(2, map.Children.Count, "Duplicate add failed");
        }

        [TestMethod]
        public void RemoveTest()
        {
            var map = new MapCanvas();
            var tile0 = new MapTile();
            var tile1 = new MapTile();
            var tile2 = new MapTile();

            map.Add(tile0);
            map.Add(tile1);

            Assert.AreEqual(2, map.Children.Count, "Add failed");

            map.Remove(null);
            Assert.AreEqual(2, map.Children.Count, "Remove null failed");

            map.Remove(tile0);
            Assert.AreEqual(1, map.Children.Count, "Remove failed");
            Assert.AreEqual(false, map.Children.Contains(tile0), "Remove failed");
            Assert.AreEqual(true, map.Children.Contains(tile1), "Remove failed");


            map.Remove(tile2);
            Assert.AreEqual(1, map.Children.Count, "Remove failed");
            Assert.AreEqual(false, map.Children.Contains(tile0), "Remove failed");
            Assert.AreEqual(true, map.Children.Contains(tile1), "Remove failed");
        }

    }
}
