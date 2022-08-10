using MealBoxCloud.Models;
using System.Linq;
using System.Web.Mvc;


namespace MealBoxCloud.Controllers
{
    public class ReportController : Controller
    {
        MealBoxesEntities Db = new MealBoxesEntities();

        // GET: Report
        public ActionResult Index()
        {

            ViewBag.ProductId = new SelectList("", "");
            ViewBag.SupplierId = new SelectList("", "");
            ViewBag.CustomerId = new SelectList("", "");
            ViewBag.AccountId = new SelectList("", "");
            ViewBag.WareHouseId = new SelectList("", "");
            ViewBag.CityId = new SelectList("", "");
            return View();
        }

        [HttpPost]
        public ActionResult Index(ReportModel model)
        {
            var ReportId = model.ReportId;

            if (ReportId == 1)
            {
                TempData["ProductId"] = model.ProductId;
                return RedirectToAction("StockSummary", "Report");
            }
            else if (ReportId == 2)
            {
                TempData["ProductId"] = model.ProductId;
                TempData["SupplierId"] = model.SupplierId;

                return RedirectToAction("Purchase", "Report");
            }
            else if (ReportId == 3)
            {
                TempData["ProductId"] = model.ProductId;
                TempData["CustomerId"] = model.CustomerId;
                return RedirectToAction("Sale", "Report");
            }
            else if (ReportId == 4)
            {
                TempData["SupplierId"] = model.SupplierId;
                return RedirectToAction("PurchaseCreadit", "Report");
            }
            else if (ReportId == 5)
            {
                TempData["CustomerId"] = model.CustomerId;
                return RedirectToAction("SaleCreadit", "Report");
            }
            else if (ReportId == 6)
            {
                return RedirectToAction("Expence", "Report");
            }
            else if (ReportId == 7)
            {
                return RedirectToAction("Profit", "Report");
            }
            else if (ReportId == 8)
            {

                TempData["AccountId"] = model.AccountId;
                TempData["PaymentMethod"] = model.PaymentMethod;
                TempData["PaymentType"] = model.PaymentType;
                return RedirectToAction("Transaction", "Report");

            }
            else if (ReportId == 9)
            {
                TempData["ProductId"] = model.ProductId;
                TempData["WareHouseId"] = model.WareHouseId;
                TempData["CityId"] = model.CityId;
                return RedirectToAction("Transaction", "Report");
            }
            return View();

        }

        public ActionResult StockSummary()
        {


            var Productid = TempData["ProductId"] as string;
            if (Productid != null)
            {

                var Data = Db.Sp_SummaryReport(Productid).ToList();

                return View(Data);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Purchase()
        {
            var Productid = TempData["ProductId"] as string;
            var Supplierid = TempData["Supplierid"] as string;

            var Data = Db.Sp_Purchase(Productid, Supplierid).ToList();

            return View(Data);
        }

        public ActionResult PurchaseCreadit()
        {
            var Supplierid = TempData["Supplierid"] as string;
            var Data = Db.Sp_PurchaseCreadit(Supplierid).ToList();

            return View(Data);
        }

        public ActionResult SaleCreadit()
        {
            var CustomerId = TempData["CustomerId"] as string;

            var Data = Db.SalCreadit(CustomerId).ToList();

            return View(Data);
        }

        public ActionResult Sale()
        {
            var Productid = TempData["ProductId"] as string;

            var CustomerId = TempData["CustomerId"] as string;

            var Data = Db.Sp_Sal(Productid, CustomerId).ToList();

            return View(Data);
        }

        public ActionResult Expence()
        {
            var Data = Db.Sp_ExpenceRpt().ToList();

            return View(Data);
        }

        public ActionResult Profit()
        {
            var Data = Db.Sp_Profit().ToList();

            return View(Data);
        }

        public ActionResult Transaction()
        {
            var AccountId = TempData["AccountId"] as string;

            var PaymentMethod = TempData["PaymentMethod"] as string;

            var PaymentType = TempData["PaymentType"] as string;

            var Data = Db.Sp_Transation(AccountId, PaymentType, PaymentMethod).ToList();

            return View(Data);
        }


        public ActionResult ProductidDDl()
        {
            var ProductId = Db.Products.Select(s => new
            {
                ProductId = s.ProductID,
                ProductName = s.ProductName
            }).ToList();

            return Json(ProductId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierDDl()
        {
            var SupplierId = Db.suppliers.Select(s => new
            {
                SupplierId = s.supplierId,
                SupplieName = s.suppliername
            }).ToList();

            return Json(SupplierId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WareHouseDDl()
        {
            var WareHouse = Db.tbl_WareHouse.Select(s => new
            {
                WareHouseId = s.WarHouseId,
                WareHousName = s.WarHouseName
            }).ToList();

            return Json(WareHouse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CityDDl()
        {
            var City = Db.Cities.Select(s => new
            {
                CityId = s.CityId,
                CityName = s.CityName
            }).ToList();

            return Json(City, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerDDl()
        {
            var CustomerId = Db.Customers_.Select(s => new
            {
                CustomersId = s.CustomerID,
                CustomersName = s.CustomerName
            }).ToList();

            return Json(CustomerId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountDDl()
        {
            var AccountId = Db.Accounts.Select(s => new
            {
                AccountCode = s.AccountGeneratedCodeId,
                AccountName = s.AccountName

            }).ToList();

            return Json(AccountId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WareHouseInventory()
        {

            var ProductId = TempData["ProductId"] as string;

            var WareHouseId = TempData["WareHouseId"] as string;

            var CityId = TempData["CityId"] as string;

            var Data = Db.Sp_WareHouseInv(ProductId, WareHouseId, CityId).ToList();


            return View(Data);
        }

    }
}