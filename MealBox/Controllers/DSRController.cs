using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBox.Models;
using MealBox.Services;

namespace MealBox.Controllers
{
    public class DSRController : Controller
    {

        private readonly DSRServices _dSRServices;

        MealBoxesEntities Db = new MealBoxesEntities();
        // GET: DSR
        public DSRController()
        {
            _dSRServices = new DSRServices();
        }

        public ActionResult AddDsr(int? id)
        {
            using(var db = new MealBoxesEntities())
            {
                var AreaList = db.tbl_area.ToList();
                ViewBag.areaid = new SelectList(AreaList, "areaid", "area_");

                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var CustomerList = db.Customers_.ToList();
                ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName");

                var BookerList = db.tbl_bkr.ToList();
                ViewBag.Salesman = new SelectList(BookerList, "bkrID", "bkrname");

                var UnitList = db.tbl_unts.ToList();
                ViewBag.untid = new SelectList(UnitList, "untid", "untnam");

                DSRModel model = new DSRModel();
                
                if(id > 0)
                {
                    var DSrId = _dSRServices.GetMdsr(id.Value);
                    var PaymentTyplist = db.tbl_PayTyp.ToList();
                    var VchTyplist = db.tbl_vchtyp.ToList();

                    ViewBag.areaid     = new SelectList(AreaList, "areaid", "area_", DSrId.areaid);
                    ViewBag.BookeriD = new SelectList(BookerList, "bkrID", "bkrname",DSrId.Salesman);
                    ViewBag.MdsrId = id;
                    ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName", DSrId.CustomerID);
                    
                    model.ttlamt = DSrId.TotalAmount;
                    model.Discount = DSrId.Discount;
                    model.TotalAfterDiscount = DSrId.TotalAfterdis;
                    model.DsrDate = DSrId.dsrdat;
                    model.Paytype = DSrId.PaymentTypeId.GetValueOrDefault();
                    model.dsrChild = db.tbl_ddsrbk.Where(w => w.MdsrId == id).

                        Select(s => new DsrChild
                        {
                            ChildProductName = s.Product.ProductName,
                            Childuntid     = s.untid,
                            ChildQty       = s.Qty,
                            Childsalrat    = s.salrat,
                            Childsalrturn  = s.salrturn,
                            ChildAmt       = s.Amt,
                            Ddsrid = s.dsrid

                        }).ToList();

                    return View(model);
                }
               
                model.dsrChild = null;
                return View(model);

            }
         
        }

        [HttpPost]
        public ActionResult AddDsr(DSRModel model)
        {
            using (var db = new MealBoxesEntities())
            {

                var AreaList = db.tbl_area.ToList();
                ViewBag.areaid = new SelectList(AreaList, "areaid", "area_");

                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var CustomerList = db.Customers_.ToList();
                ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName");

                var BookerList = db.tbl_bkr.ToList();
                ViewBag.Salesman = new SelectList(BookerList, "bkrID", "bkrname");

                var UnitList = db.tbl_unts.ToList();
                ViewBag.untid = new SelectList(UnitList, "untid", "untnam");
 

                tbl_Mdsr obj = new tbl_Mdsr();
                obj.CustomerID = model.CustomerID;
                obj.areaid = model.areaid;
                obj.Salesman = model.Salesman;
                obj.furout = model.furout;
                obj.PaymentTypeId = model.Paytype;
                obj.Discount = model.Discount.GetValueOrDefault();
                obj.TotalAmount = model.ttlamt;
                obj.dsrdat = model.DsrDate;
                obj.DsrRemarks = model.dsrrmk;
                obj.TotalAfterdis = model.TotalAfterDiscount;
                obj.BookerId = model.BookeriD;
                db.tbl_Mdsr.Add(obj);
                db.SaveChanges();

                if(model.dsrChild != null)
                {
                    foreach(var item in model.dsrChild)
                    {
                        tbl_ddsrbk obj2 = new tbl_ddsrbk();
                        {
                            obj2.ProductID = item.ChildProductID;
                            obj2.salrat = item.Childsalrat;
                            obj2.Qty = item.ChildQty.GetValueOrDefault();
                            obj2.Amt = item.ChildAmt;
                            obj2.untid = item.Childuntid;
                            obj2.MdsrId = obj.dsrid;
                            obj2.salrturn = item.Childsalrturn;
                            db.tbl_ddsrbk.Add(obj2);
                            db.SaveChanges();
                        }
                          var DsrStockUpdate = _dSRServices.GetDsrStock(item.ChildProductID.GetValueOrDefault());
                          DsrStockUpdate.DsrQuantity = DsrStockUpdate.DsrQuantity +  item.ChildQty;
                          DsrStockUpdate.PurchaseDate = model.DsrDate;
                         _dSRServices.UpdateDsrStock(DsrStockUpdate);
                    }
                  }
                }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DsrList() 
        {
            var DsrList = _dSRServices.GetDsrList().ToList();
            return View(DsrList);
        }

        public ActionResult VerifyDsr() 
        {
            return View();
        }

        public ActionResult FinalDsr()
        {
            return View();
        }

        public ActionResult DsrView() 
        {
            return View();
        }
    }
}