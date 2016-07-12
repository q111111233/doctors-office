using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DoctorsOffice
{
  public class Patient
  {
    public int id;
    public string name;
    public Doctor doc;
    public Patient(string Name, Doctor doctor, int ID=0)
    {
      id = ID;
      name = Name;
      doc = doctor;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patients (name, doctor_id) OUTPUT INSERTED.id VALUES (@Name, @DoctorId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
      nameParameter.Value = name;

      SqlParameter DoctorIdParameter = new SqlParameter();
      DoctorIdParameter.ParameterName = "@DoctorId";
      DoctorIdParameter.Value = doc.id;

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(DoctorIdParameter);

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


  }
}
