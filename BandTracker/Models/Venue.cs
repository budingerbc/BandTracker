using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string name, int id = 0)
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

    public override bool Equals(Object otherVenue)
    {
      if(!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue band = (Venue) otherVenue;
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
      cmd.CommandText = @"INSERT INTO venues (name) VALUES (@name);";

      MySqlParameter venueName = new MySqlParameter();
      venueName.ParameterName = "@name";
      venueName.Value = _name;
      cmd.Parameters.Add(venueName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddBand(Band newBand)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@bandId, @thisId);";

      MySqlParameter bandId = new MySqlParameter();
      bandId.ParameterName = "@bandId";
      bandId.Value = newBand.GetId();
      cmd.Parameters.Add(bandId);

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@thisId";
      venueId.Value = _id;
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Band> GetBandHistory()
    {
      List<Band> bandsPlayed = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT bands.* FROM venues
      JOIN bands_venues ON (venues.id = bands_venues.venue_id)
      JOIN bands ON (bands_venues.band_id = bands.id)
      WHERE venues.id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Band newBand = new Band(name, id);
        bandsPlayed.Add(newBand);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return bandsPlayed;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues WHERE id = @thisId; DELETE FROM bands_venues WHERE venue_id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Update(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET name = @name WHERE id = @thisId;";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@thisId";
      id.Value = _id;
      cmd.Parameters.Add(id);

      _name = newName;

      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Venue Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @thisId;";

      MySqlParameter venue = new MySqlParameter();
      venue.ParameterName = "@thisid";
      venue.Value = id;
      cmd.Parameters.Add(venue);

      int venueId = 0;
      string venueName = "";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
      }

      Venue foundVenue = new Venue(venueName, venueId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundVenue;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Venue newVenue = new Venue(name, id);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues; DELETE FROM bands_venues;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
