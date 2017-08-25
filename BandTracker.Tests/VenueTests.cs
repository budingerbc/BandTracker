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

    [TestMethod]
    public void GetBandHistory_GetsAllBandsThatPlayedAtVenue_BandList()
    {
      Venue newVenue = new Venue("Gorge Amphitheatre");
      Band one = new Band("Armin Van Buuren");
      Band two = new Band("Porter Robinson");
      Band three = new Band("Morgan Page");

      newVenue.Save();
      one.Save();
      two.Save();
      three.Save();

      newVenue.AddBand(one);
      newVenue.AddBand(two);

      List<Band> expected = new List<Band> {one, two};
      List<Band> actual = newVenue.GetBandHistory();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Update_UpdatesVenueNameInDatabase_VenueName()
    {
      Venue newVenue = new Venue("Gorge Amphitheatre");
      string expected = "Red Rocks";
      newVenue.Update(expected);

      string actual = newVenue.GetName();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesVenueInDB_VenueList()
    {
      Venue one = new Venue("Gorge Amphitheatre");
      Venue two = new Venue("Red Rocks");
      Venue three = new Venue("WaMu");

      one.Save();
      two.Save();
      three.Save();
      two.Delete();

      List<Venue> expected = new List<Venue> {one, three};
      List<Venue> actual = Venue.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesVenueBandLinkInDB_VenueList()
    {
      Venue testVenue = new Venue("Gorge Amphitheatre");
      Venue keepVenue = new Venue("Red Rocks");
      Band testBand = new Band("Seven Lions");

      testVenue.Save();
      keepVenue.Save();
      testBand.Save();

      testVenue.AddBand(testBand);
      keepVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> expected = new List<Venue> {keepVenue};
      List<Venue> actual = testBand.GetVenuesPlayedAt();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabaseById_VenueId()
    {
      Venue newVenue = new Venue("Red Rocks");
      newVenue.Save();

      int expected = newVenue.GetId();
      int actual = Venue.Find(newVenue.GetId()).GetId();

      Assert.AreEqual(expected, actual);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
