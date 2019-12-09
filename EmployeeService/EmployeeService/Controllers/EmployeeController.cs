using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess1;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Employee> Get() {

            using(EmployeeDBEntities enitites=new EmployeeDBEntities())
            {
                return enitites.Employees.ToList();
            }

        }

        public Employee Get(int id)
        {
            using (EmployeeDBEntities enitites = new EmployeeDBEntities())
            {
                return enitites.Employees.FirstOrDefault(e=>e.ID==id);
            }
        }
    }
}
