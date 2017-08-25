using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System.Collections.Generic;
using System;

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

      return View("Venues", Venue.GetAll());
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
  }
}
