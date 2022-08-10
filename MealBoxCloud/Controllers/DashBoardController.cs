using MealBoxCloud.Class;
using MealBoxCloud.Models;
using MealBoxCloud.Services;
using System.Linq;
using System.Web.Mvc;

namespace MealBoxCloud.Controllers
{
    public class DashBoardController : Controller
    {

        MealBoxesEntities Db = new MealBoxesEntities();
        // GET: DashBoard
        private readonly DashBoardService _dashBoardService;
        public DashBoardController()
        {
            _dashBoardService = new DashBoardService();
        }
        public ActionResult Index()
        {
            ViewBag.BufferStock = _dashBoardService.GetBufferStock();
            ViewBag.CustomerNo = _dashBoardService.GetCustomerNo();
            ViewBag.BookerNO = _dashBoardService.GetBookerNo();
            ViewBag.SupplierNo = _dashBoardService.GetSupplier();

            return View();
        }

        public ActionResult SaleReturn(int? id)
        {

            PosModel Model = new PosModel();

            using (var db = new MealBoxesEntities())
            {
                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var SaleReturnList = db.tbl_MSal.ToList();
                ViewBag.InvoiceNum = new SelectList(SaleReturnList, "MSal_id", "MSal_sono");

            }


            ViewBag.Accno = new SelectList("", "");

            Model.posChildren = null;

            return View(Model);
        }

        [HttpPost]
        public ActionResult SaleReturn(PosModel Model)
        {
            return View();
        }


        public ActionResult Authorize()
        {
            var EmployeeList = Db.tbl_employee.ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "employeeID", "employeeName");

            var UserTypeListList = Db.tbl_userType.ToList();
            ViewBag.UserTypeId = new SelectList(UserTypeListList, "UserTypeid", "UserType");

            return View();
        }

        [HttpPost]
        public ActionResult Authorize(LoginSinUpModel model)
        {
            var EmployeeList = Db.tbl_employee.ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "employeeID", "employeeName");

            var UserTypeListList = Db.tbl_userType.ToList();
            ViewBag.UserTypeId = new SelectList(UserTypeListList, "UserTypeid", "UserType");

            tbl_User obj = new tbl_User();
            obj.UserName = model.UserName;
            obj.UserPassword = Cryptography.Encrypt(model.Password);
            obj.UserId = model.UserId;
            obj.UserTypeId = model.UserTypeId;
            obj.EmployeeId = model.EmployeeId;
            obj.Name = model.Name;
            obj.Normalpassword = model.Password;
            Db.tbl_User.Add(obj);
            Db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
