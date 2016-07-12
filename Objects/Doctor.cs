using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DoctorsOffice
{
  public class Doctor
  {
    public int id;
    public string name;
    public Specialty specialty;
    public Doctor(string Name, Specialty spec, int ID=0)
    {
      id = ID;
      name = Name;
      specialty = spec;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO doctors (name, specialty_id) OUTPUT INSERTED.id VALUES (@Name, @SpecialtyId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
      nameParameter.Value = name;

      SqlParameter SpecialtyIdParameter = new SqlParameter();
      SpecialtyIdParameter.ParameterName = "@SpecialtyId";
      SpecialtyIdParameter.Value = specialty.id;

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(SpecialtyIdParameter);

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
    public List<Patient> GetPatients()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients WHERE doctor_id=@thisId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;

      cmd.Parameters.Add(idParameter);

      rdr = cmd.ExecuteReader();

      List<Patient> patients = new List<Patient> {};
      while(rdr.Read())
      {
        Patient p = new Patient(rdr.GetString(1), this, rdr.GetInt32(0));
        patients.Add(p);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return patients;
    }
    public int CountPatients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM patients WHERE doctor_id=@thisId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;

      cmd.Parameters.Add(idParameter);

      int result = (int)cmd.ExecuteScalar();

      if (conn != null)
      {
        conn.Close();
      }

      return result;
    }
  }
}
