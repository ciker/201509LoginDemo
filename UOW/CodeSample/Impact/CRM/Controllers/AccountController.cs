using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Service;
using WebMatrix.WebData;
using CRM.Filters;
using CRM.Models;

namespace CRM.Controllers
{

    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private IContactService _contactService;

        public AccountController(IAccountService accountService, IContactService contactService)
        {
            this._accountService = accountService;
            this._contactService = contactService;
        }

        public ActionResult Index()
        {
            var accounts = _accountService.GetAll();
            ViewBag.TotalRecords = accounts.Count();
            return View(accounts);
        }
        public ActionResult Edit()
        {
            return View();
        }

        public JsonResult GetAccounts(string search, string sidx, string sord, int page, int rows)
        {


            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            long totalRecords = 0;
            var accounts = _accountService.GetAll();

            totalRecords = accounts.Count();
            var acccountList = accounts.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                from e in acccountList
                select new
                {
                    Id = e.Id,
                    Name = e.Name,
                    Url = e.Url,
                    Phone = e.Phone,
                    Email = e.Email,
                    AccountType = e.AccountType.ToString(),
                    AccountStatus = e.AccountStatus.ToString()

                }).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PerformCRUDAction(Account account)
        {

            bool result = false;
            switch (Request.Form["oper"])
            {
                case "add":
                    result = _accountService.Add(account);
                    break;
                case "edit":
                    int id = Int32.Parse(Request.Form["id"]);
                    account.Id = id;
                    result = _accountService.Update(account);
                    break;
                case "del":
                    id = Int32.Parse(Request.Form["id"]);
                    account.Id = id;
                  //  result = _accountService.Delete(account);

                    break;
                default:
                    break;
            }
            return Json(result);
        }


    }
}
