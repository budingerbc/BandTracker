using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public override bool Equals(Object otherBand)
    {
      if(!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band band = (Band) otherBand;
        bool idEquality = _id == band.GetId();
        bool nameEquality = _name == band.GetName();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return _name.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (name) VALUES (@name);";

      MySqlParameter bandName = new MySqlParameter();
      bandName.ParameterName = "@name";
      bandName.Value = _name;
      cmd.Parameters.Add(bandName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddVenue(Venue newVenue)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@thisId, @venueId);";

      MySqlParameter bandId = new MySqlParameter();
      bandId.ParameterName = "@thisId";
      bandId.Value = _id;
      cmd.Parameters.Add(bandId);

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@venueId";
      venueId.Value = newVenue.GetId();
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Venue> GetVenuesPlayedAt()
    {
      List<Venue> venuesPlayed = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venues.* FROM bands
      JOIN bands_venues ON (bands.id = bands_venues.band_id)
      JOIN venues ON (bands_venues.venue_id = venues.id)
      WHERE bands.id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Venue newVenue = new Venue(name, id);
        venuesPlayed.Add(newVenue);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return venuesPlayed;
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Band newBand = new Band(name, id);
        allBands.Add(newBand);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands; DELETE FROM bands_venues;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
