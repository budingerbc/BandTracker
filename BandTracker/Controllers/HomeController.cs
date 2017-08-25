using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View(Venue.GetAll());
    }

    [HttpGet("/venues")]
    public ActionResult Venues()
    {
      return View("Index", Venue.GetAll());
    }

    [HttpGet("/venue-form")]
    public ActionResult VenueForm()
    {
      return View();
    }

    [HttpPost("/venue-added")]
    public ActionResult VenueAdded()
    {
      Venue newVenue = new Venue(Request.Form["venue-name"]);
      newVenue.Save();

      return View("Index", Venue.GetAll());
    }

    [HttpGet("/venue-details/{id}")]
    public ActionResult VenueDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};

      Venue foundVenue = Venue.Find(id);
      List<Band> allBands = Band.GetAll();
      List<Band> venueBands = foundVenue.GetBandHistory();

      List<Band> uniqueBands = allBands.Except(venueBands).ToList();

      model.Add("venue", foundVenue);
      model.Add("bands", uniqueBands);

      return View(model);
    }

    [HttpGet("/venue-details/{id}/edit")]
    public ActionResult EditVenue(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};

      Venue editVenue = Venue.Find(id);

      return View(editVenue);
    }

    [HttpPost("/venue-details/{id}/updated")]
    public ActionResult VenueDetailsEdited(int id)
    {
      Venue updateVenue = Venue.Find(id);
      updateVenue.Update(Request.Form["venue-name"]);

      Dictionary<string, object> model = new Dictionary<string, object> {};

      Venue foundVenue = Venue.Find(id);
      List<Band> allBands = Band.GetAll();
      List<Band> venueBands = foundVenue.GetBandHistory();

      List<Band> uniqueBands = allBands.Except(venueBands).ToList();

      model.Add("venue", foundVenue);
      model.Add("bands", uniqueBands);

      return View("VenueDetails", model);
    }


    [HttpPost("/venue-details/{id}/bands-added")]
    public ActionResult VenueDetailsBandsAdded(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};

      Venue updatedVenue = Venue.Find(id);

      string bandValues = Request.Form["bands"];
      string[] split = bandValues.Split(',');

      foreach(var bandId in split)
      {
        updatedVenue.AddBand(Band.Find(int.Parse(bandId)));
      }

      List<Band> allBands = Band.GetAll();
      List<Band> venueBands = updatedVenue.GetBandHistory();

      List<Band> unqiueBands = allBands.Except(venueBands).ToList();

      model.Add("venue", updatedVenue);
      model.Add("bands", unqiueBands);

      return View("VenueDetails", model);
    }

    [HttpPost("/venue-deleted/{id}")]
    public ActionResult VenueDeleted(int id)
    {
      Venue deleteVenue = Venue.Find(id);
      deleteVenue.Delete();

      return View("Index", Venue.GetAll());
    }


    [HttpGet("/bands")]
    public ActionResult Bands()
    {
      return View(Band.GetAll());
    }

    [HttpGet("/band-form")]
    public ActionResult BandForm()
    {
      return View();
    }

    [HttpPost("/band-added")]
    public ActionResult BandAdded()
    {
      Band newBand = new Band(Request.Form["band-name"]);
      newBand.Save();

      return View("Bands", Band.GetAll());
    }

    [HttpGet("/band-details/{id}")]
    public ActionResult BandDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};

      Band foundBand = Band.Find(id);
      List<Venue> allVenues = Venue.GetAll();
      List<Venue> venuesPlayed = foundBand.GetVenuesPlayedAt();

      List<Venue> uniqueVenues = allVenues.Except(venuesPlayed).ToList();

      model.Add("band", foundBand);
      model.Add("venues", uniqueVenues);
      model.Add("venuesPlayed", venuesPlayed);

      return View(model);
    }

    [HttpGet("/band-details/{id}/edit")]
    public ActionResult EditBand(int id)
    {
      Band editBand = Band.Find(id);
      return View(editBand);
    }

    [HttpPost("/band-details/{id}/updated")]
    public ActionResult BandDetailsEdited(int id)
    {
      Band updateBand = Band.Find(id);
      updateBand.Update(Request.Form["band-name"]);

      Dictionary<string, object> model = new Dictionary<string, object> {};

      Band foundBand = Band.Find(id);
      List<Venue> allVenues = Venue.GetAll();
      List<Venue> venuesPlayed = foundBand.GetVenuesPlayedAt();

      List<Venue> uniqueVenues = allVenues.Except(venuesPlayed).ToList();

      model.Add("band", foundBand);
      model.Add("venues", uniqueVenues);
      model.Add("venuesPlayed", venuesPlayed);

      return View("BandDetails", model);
    }

    [HttpPost("/band-details/{id}/venues-added")]
    public ActionResult BandDetailsVenuesAdded(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};

      Band updatedBand = Band.Find(id);

      string venuesValues = Request.Form["venues"];
      string[] split = venuesValues.Split(',');

      foreach(var venueId in split)
      {
        updatedBand.AddVenue(Venue.Find(int.Parse(venueId)));
      }

      List<Venue> allVenues = Venue.GetAll();
      List<Venue> venuesPlayed = updatedBand.GetVenuesPlayedAt();

      List<Venue> uniqueVenues = allVenues.Except(venuesPlayed).ToList();

      model.Add("band", updatedBand);
      model.Add("venues", uniqueVenues);
      model.Add("venuesPlayed", venuesPlayed);

      return View("BandDetails", model);
    }

    [HttpPost("/band-deleted/{id}")]
    public ActionResult BandDeleted(int id)
    {
      Band deleteMe = Band.Find(id);
      deleteMe.Delete();

      return View("Bands", Band.GetAll());
    }
  }
}
