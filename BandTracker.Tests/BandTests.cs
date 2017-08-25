using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllBandsInEmptyDatabase_0()
    {
      int expected = 0;
      int actual = Band.GetAll().Count;

      Assert.AreEqual(expected, actual);
    }

    public void Dispose()
    {

    }
  }
}
