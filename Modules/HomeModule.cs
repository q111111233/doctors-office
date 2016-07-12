using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace DoctorsOffice
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Specialty> specialties = Specialty.GetAll();
        return View["index.cshtml", specialties];
      };
      Get["/specialties/{id}"] = parameters => {
        List<Specialty> specialties = Specialty.GetAll();
        foreach(Specialty s in specialties)
        {
          if(s.id == parameters.id)
          {
            return View["doctors.cshtml", s];
          }
        }
        return View["doctors.cshtml"];
      };
      Post["/doctors/new"] = _ => {
        int s_id = Request.Form["spec"];
        List<Specialty> specialties = Specialty.GetAll();
        foreach(Specialty s in specialties)
        {
          if(s.id == s_id)
          {
            Doctor d = new Doctor(Request.Form["name"], s);
            d.Save();
            return View["doctors.cshtml", s];
          }
        }
        return View["doctors.cshtml"];
      };
      Get["/{sid}/doctors/{did}"] = parameters => {
        List<Specialty> specialties = Specialty.GetAll();
        foreach(Specialty s in specialties)
        {
          if(s.id == parameters.sid)
          {
            List<Doctor> doctors = s.GetDoctors();
            foreach(Doctor d in doctors)
            {
              if(d.id == parameters.did)
              {
                return View["patients.cshtml", d];
              }
            }
          }
        }
        return View["patients.cshtml"];
      };
      Post["/patients/new"] = _ => {
        int s_id = Request.Form["spec"];
        int d_id = Request.Form["doc"];
        List<Specialty> specialties = Specialty.GetAll();
        foreach(Specialty s in specialties)
        {
          if(s.id == s_id)
          {
            List<Doctor> doctors = s.GetDoctors();
            foreach(Doctor d in doctors)
            {
              if(d.id == d_id)
              {
                Patient p = new Patient(Request.Form["name"], d);
                p.Save();
                return View["patients.cshtml", d];
              }
            }
          }
        }
        return View["patients.cshtml"];
      };
    }
  }
}
