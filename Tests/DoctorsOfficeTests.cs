using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DoctorsOffice
{
  public class CategoryTest
  {
    [Fact]
    public void DoctorsForSpecialty()
    {
      Specialty s = new Specialty("surgery");
      Doctor d = new Doctor("Don", s);
      d.Save();
      Doctor r = new Doctor("Ron", s);
      r.Save();
      Patient p = new Patient("Peter", d);
      p.Save();
      Patient o = new Patient("Pedro", d);
      o.Save();

      Assert.Equal(2, d.GetPatients().Count);
    }
  }
}
