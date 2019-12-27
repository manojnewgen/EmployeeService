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
        [HttpGet]
        public HttpResponseMessage LoadEmployeeData(string gender="ALL") {

            using(EmployeeDBEntities enitites=new EmployeeDBEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, enitites.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, enitites.Employees.Where(e=>e.Gender.ToLower()== "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, enitites.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                    default: return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Value for gender must be ALL, Female and Male your query string for gender is ={gender} ");

                }
            }

        }
        [HttpGet]
        public HttpResponseMessage LoadEmployeeByID(int id)
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

        public HttpResponseMessage Delete(int id)
        {
            using (EmployeeDBEntities enitites = new EmployeeDBEntities())
            {
                try
                {
                    var en = enitites.Employees.FirstOrDefault(e => e.ID == id);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"record with id: {id} not found");
                    }
                    else
                    {
                        enitites.Employees.Remove(en);
                        enitites.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

                         
                }
               
               
            }
        }

        public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with ID={id} not found");
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch(Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }
    }
}
