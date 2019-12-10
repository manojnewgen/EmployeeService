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

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities enitites = new EmployeeDBEntities())
            {
                var en= enitites.Employees.FirstOrDefault(e=>e.ID==id);
                if (en != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Found, en);
                }
                else
                {
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data with id=" + id + " " + "not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody]Employee employee)
        {
           try
            {
                using (EmployeeDBEntities enitites = new EmployeeDBEntities())
                {
                    enitites.Employees.Add(employee);
                    enitites.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
