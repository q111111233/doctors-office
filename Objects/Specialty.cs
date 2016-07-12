using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DoctorsOffice
{
  public class Specialty
  {
    public int id;
    public string name;
    public Specialty(string Name, int ID=0)
    {
      id = ID;
      name = Name;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO specialties (name) OUTPUT INSERTED.id VALUES (@Name);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
      nameParameter.Value = name;

      cmd.Parameters.Add(nameParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public List<Doctor> GetDoctors()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM doctors WHERE specialty_id=@thisId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;

      cmd.Parameters.Add(idParameter);

      rdr = cmd.ExecuteReader();

      List<Doctor> doctors = new List<Doctor> {};
      while(rdr.Read())
      {
        Doctor d = new Doctor(rdr.GetString(1), this, rdr.GetInt32(0));
        doctors.Add(d);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return doctors;
    }
    public static List<Specialty> GetAll()
    {
      {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr;
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM specialties;", conn);

        rdr = cmd.ExecuteReader();

        List<Specialty> specialties = new List<Specialty> {};
        while(rdr.Read())
        {
          Specialty s = new Specialty(rdr.GetString(1), rdr.GetInt32(0));
          specialties.Add(s);
        }

        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }

        return specialties;
      }
    }
  }
}
