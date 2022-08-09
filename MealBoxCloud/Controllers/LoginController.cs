using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBoxCloud.Class;
using MealBoxCloud.Models;

namespace MealBoxCloud.Controllers
{
    public class LoginController : Controller
    {
        MealBoxesEntities db = new MealBoxesEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginSinUpModel model)
        {
          
                var pas = Cryptography.Encrypt(model.Password);
                var q = db.tbl_User.Where(x => x.UserName == model.UserName && x.UserPassword == pas).FirstOrDefault();
                if (q != null)
                {
                    var password = q.UserPassword;
                     var apppas = pas;
                    if (pas == apppas)
                    {
                    Session["UserTypeId"] = q.UserTypeId;
                    Session["EmployeeId"] = q.EmployeeId;
                    Session["Username"]   =   q.UserName;
                    return RedirectToAction("Index", "DashBoard", new { Empid = Session["EmployeeId"] });
                    }
                else
                {
                    ViewBag.Msg = "User Name or Password is in not valid";
                    return View("Index");
                }
            }
            else
            {
                return View();
            }
        
          
        }
    }
}