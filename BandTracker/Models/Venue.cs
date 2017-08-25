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
