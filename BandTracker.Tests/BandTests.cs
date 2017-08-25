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

    [TestMethod]
    public void Find_FindsBandInDatabaseById_BandId()
    {
      Band newBand = new Band("Seven Lions");
      newBand.Save();

      int expected = newBand.GetId();
      int actual = Band.Find(newBand.GetId()).GetId();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Update_UpdatesBandNameInDatabase_Band()
    {
      Band newBand = new Band("Seven Lions");
      newBand.Save();

      newBand.Update("Seven Lions & Jason Ross");
      Band expected = newBand;
      Band actual = Band.GetAll()[0];

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesBandFromDatabase_BandList()
    {
      Band one = new Band("one");
      Band two = new Band("two");
      Band three = new Band("three");

      one.Save();
      two.Save();
      three.Save();

      two.Delete();

      List<Band> expected = new List<Band> {one, three};
      List<Band> actual = Band.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
