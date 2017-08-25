using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Client
  {
    private int _id;
    private string _name;

    public Client(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(Object otherBand)
    {
      if(!(otherBand is Band))
      {
        return false;
      }
      else
      {
        otherBand = (Band) otherBand;
      }
    }
  }
