using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdvanced.GenericRepository;

namespace MvcAdvanced.Controllers
{
    public class PermissionController : Controller
    {
        private UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork();
       
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllEmpRole()
        {
            var employeeList = unitOfWork.EmpRoleRepository.Get();
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetAllEmpNames()
        {
            var employeeList = unitOfWork.EmployeeRepository.Get();
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllRoles()
        {
            var roleList = (from emp in unitOfWork.EmployeeRepository.Get() join role in unitOfWork.EmpRoleRepository.Get() on emp.RoleID equals role.ID select new { emp.ID, emp.FirstName, emp.LastName, emp.UserName, role.Role }).ToList();
            return Json(roleList, JsonRequestBehavior.AllowGet);
        }

        public string UpdateRole(Employee permission)
        {

            if (permission != null)
            {
                var emp = unitOfWork.EmployeeRepository.GetByID(permission.ID);
                emp.RoleID = permission.RoleID;
               
                unitOfWork.EmployeeRepository.Update(emp);
                unitOfWork.Save();

                return "Record has been Updated";
            }
            else
            {
                return "Record has Not been Updated";
            }
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
