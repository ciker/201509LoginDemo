using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdvanced.GenericRepository;
using AutoMapper;

namespace MvcAdvanced.Controllers
{
    public class EmployeeController : Controller
    {
        private UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork();
           
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllEmployees()
        {
            var employeeList = (List<Employee>)unitOfWork.EmployeeRepository.Get();
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        public string Update(Employee employee)
        { 
            if (employee != null)
            {
                var emp = unitOfWork.EmployeeRepository.GetByID(employee.ID);
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.UserName = employee.UserName;
               
                unitOfWork.EmployeeRepository.Update(emp);
                unitOfWork.Save();                    
               
               return "Record has been Updated";
               
            }
            else
            {
                return "Record has Not been Updated";
            }
        }

        public string Delete(int id)
        {
            try
            {
                if (id != null)
                {
                    unitOfWork.EmployeeRepository.Delete(id);
                    unitOfWork.Save();

                    return "Employee Has Been Deleted";
                }
                else
                {
                    return "Employee Hasnot Been Deleted";
                }
            }
            catch
            {
                return "Employee Hasnot Been Deleted";
            }
        }

        public string Add(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    unitOfWork.EmployeeRepository.Insert(employee);
                    unitOfWork.Save();
                    return "Record has been Added";
                }
                else
                {
                    return "Record has Not been Verified";
                }
            }

            catch
            {
                return "Record has Not been Added";
            }
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
               
    }
}
