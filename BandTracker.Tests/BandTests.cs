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

    [TestMethod]
    public void Save_SavesBandToDatabase_Band()
    {
      Band expected = new Band("A&B");
      expected.Save();

      Band actual = Band.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetVenuesPlayedAt_GetsAllVenuesABandHasPlayedAt_VenueList()
    {
      Band newBand = new Band("Seven Lions");
      Venue one = new Venue("Gorge Amphitheatre");
      Venue two = new Venue("The Showbox");
      Venue three = new Venue("Red Rocks");

      newBand.Save();
      one.Save();
      two.Save();
      three.Save();

      newBand.AddVenue(one);
      newBand.AddVenue(two);

      List<Venue> expected = new List<Venue> {one, two};
      List<Venue> actual = newBand.GetVenuesPlayedAt();

      CollectionAssert.AreEqual(expected, actual);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
