using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_test;";
    }

    [TestMethod]
    public void GetAll_GetsAllVenuesInEmptyDatabase_0()
    {
      int expected = 0;
      int actual = Venue.GetAll().Count;

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Save_SavesVenueToDatabase_Venue()
    {
      Venue expected = new Venue("Gorge Amphitheatre");
      expected.Save();

      Venue actual = Venue.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
