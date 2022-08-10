using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBoxCloud.Models;
using MealBox.Services;
using MealBoxCloud;

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

                var BookerList = db.tbl_User.Where(w => w.UserTypeId == 2).Select(s => new
                {
                    Id = s.EmployeeId,
                    Name = s.Name

                }).ToList();

                ViewBag.BookeriD = new SelectList(BookerList, "Id", "Name");

                var UnitList = db.tbl_unts.ToList();
                ViewBag.untid = new SelectList(UnitList, "untid", "untnam");

                DSRModel model = new DSRModel();
                

                if (id > 0)
                {
                    var DSrId = _dSRServices.GetMdsr(id.Value);
                    var PaymentTyplist = db.tbl_PayTyp.ToList();
                    var VchTyplist = db.tbl_vchtyp.ToList();
                    ViewBag.BookeriD = new SelectList("", "");
                    ViewBag.areaid     = new SelectList(AreaList, "areaid", "area_", DSrId.areaid);
                    ViewBag.BookeriD = new SelectList(BookerList, "Id", "Name", DSrId.BookerId);
                    //ViewBag.MdsrId = id;
                    ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName", DSrId.CustomerID);
                    
                    model.ttlamt = DSrId.TotalAmount;
                    model.Discount = DSrId.Discount;
                    model.TotalAfterDiscount = DSrId.TotalAfterdis;
                    model.DsrDate = DSrId.dsrdat;
                    model.Paytype = DSrId.PaymentTypeId.GetValueOrDefault();
                    model.DsriD = id.Value;
                    model.dsrrmk = DSrId.DsrRemarks;
                    model.furout = DSrId.furout;
                    model.dsrChild = db.tbl_ddsrbk.Where(w => w.MdsrId == id).

                        Select(s => new DsrChild
                        {
                            ChildProductName = s.Product.ProductName,
                            ChildProductID = s.ProductID,
                            Childuntid     = s.untid,
                            ChildQty       = s.Qty,
                            Childsalrat    = s.salrat,
                            Childsalrturn  = s.salrturn,
                            ChildAmt       = s.Amt,
                            Ddsridchild = s.dsrid,
                            ChildUnitName = s.tbl_unts.untnam
                            

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
            using (var db = new MealBoxesEntities1())
            {

                var AreaList = db.tbl_area.ToList();
                ViewBag.areaid = new SelectList(AreaList, "areaid", "area_");

                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var CustomerList = db.Customers_.ToList();
                ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName");

                var UnitList = db.tbl_unts.ToList();
                ViewBag.untid = new SelectList(UnitList, "untid", "untnam");

                var SessionId = Convert.ToInt32(@Session["UserTypeId"]);
                var DsrId = model.DsriD;
                var BookerId = model.BookeriD;

                if (DsrId == 0 && SessionId == 2)
                {
                    tbl_Mdsr obj = new tbl_Mdsr();
                    obj.CustomerID = model.CustomerID;
                    obj.areaid = model.areaid;
                    obj.PaymentTypeId = model.Paytype;
                    obj.Discount = model.Discount.GetValueOrDefault();
                    obj.TotalAmount = model.ttlamt;
                    obj.dsrdat = model.DsrDate;
                    obj.DsrRemarks = model.dsrrmk;
                    obj.TotalAfterdis = model.TotalAfterDiscount;
                    obj.BookerId = model.BookeriD;
                    db.tbl_Mdsr.Add(obj);
                    db.SaveChanges();

                    if (model.dsrChild != null)
                    {
                        foreach (var item in model.dsrChild)
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
                                obj2.DsrBookid = model.BookeriD;
                                obj2.DsrDate = model.DsrDate;
                                db.tbl_ddsrbk.Add(obj2);
                                db.SaveChanges();
                            }

                            var DsrStockUpdate = _dSRServices.GetDsrStock(item.ChildProductID.GetValueOrDefault());
                            DsrStockUpdate.DsrQuantity = DsrStockUpdate.DsrQuantity + item.ChildQty;
                            DsrStockUpdate.PurchaseDate = model.DsrDate;
                            _dSRServices.UpdateDsrStock(DsrStockUpdate);
                        }
                    }
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else if (DsrId > 0 && SessionId == 2)
                {
                    var Mdsr = _dSRServices.GetMdsr(DsrId);
                    Mdsr.CustomerID = model.CustomerID;
                    Mdsr.areaid = model.areaid;
                    Mdsr.PaymentTypeId = model.Paytype;
                    Mdsr.Discount = model.Discount.GetValueOrDefault();
                    Mdsr.TotalAmount = model.ttlamt;
                    Mdsr.dsrdat = model.DsrDate;
                    Mdsr.DsrRemarks = model.dsrrmk;
                    Mdsr.TotalAfterdis = model.TotalAfterDiscount;
                    Mdsr.BookerId = model.BookeriD;
                    db.SaveChanges();

                    if (model.deleteItemChildren != null)
                    {
                        foreach (var itemdel in model.deleteItemChildren)
                        {
                            var del = db.tbl_ddsrbk.Where(w => w.dsrid == itemdel.Ddsriddelte).FirstOrDefault();
                            db.tbl_ddsrbk.Remove(del);
                        }
                        db.SaveChanges();
                    }

                    if (model.dsrChild != null)
                    {
                        foreach (var item in model.dsrChild)
                        {
                            tbl_ddsrbk obj2 = new tbl_ddsrbk();
                            var del = db.tbl_ddsrbk.Where(w => w.dsrid == item.Ddsridchild).FirstOrDefault();
                            if (del != null)
                            {
                                db.tbl_ddsrbk.Remove(del);
                            }
                            obj2.ProductID = item.ChildProductID;
                            obj2.salrat = item.Childsalrat;
                            obj2.Qty = item.ChildQty;
                            obj2.Amt = item.ChildAmt;
                            obj2.untid = item.Childuntid;
                            obj2.MdsrId = DsrId;
                            obj2.DsrBookid = BookerId;
                            obj2.DsrDate = model.DsrDate;
                            db.tbl_ddsrbk.Add(obj2);
                            db.SaveChanges();


                            var DsrStockUpdate = _dSRServices.GetDsrStock(item.ChildProductID.GetValueOrDefault());
                            DsrStockUpdate.DsrQuantity = 0;
                            DsrStockUpdate.DsrQuantity = DsrStockUpdate.DsrQuantity + item.ChildQty;
                            DsrStockUpdate.PurchaseDate = model.DsrDate;
                            _dSRServices.UpdateDsrStock(DsrStockUpdate);

                        }
                    }
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else if (DsrId > 0 && SessionId == 3)
                {
                    var Mdsr2 = _dSRServices.GetMdsr(DsrId);
                    Mdsr2.Salesman = model.Salesman;
                    Mdsr2.furout = model.furout;
                    Mdsr2.DsrRemarks = model.dsrrmk;
                    BookerId = Mdsr2.BookerId.Value;
                    _dSRServices.Updatemdsr(Mdsr2);



                    if (model.dsrChild != null)
                    {
                        foreach (var item in model.dsrChild)
                        {
                            tbl_ddsrbk obj2 = new tbl_ddsrbk();
                            var del = db.tbl_ddsrbk.Where(w => w.dsrid == item.Ddsridchild).FirstOrDefault();
                            if (del != null)
                            {
                                db.tbl_ddsrbk.Remove(del);
                            }
                            obj2.ProductID = item.ChildProductID;
                            obj2.salrat = item.Childsalrat;
                            obj2.Qty = item.ChildQty;
                            obj2.Amt = item.ChildAmt;
                            obj2.untid = item.Childuntid;
                            obj2.MdsrId = DsrId;
                            obj2.DsrBookid = BookerId;
                            obj2.salrturn = item.Childsalrturn;
                            obj2.DsrDate = model.DsrDate;
                          db.tbl_ddsrbk.Add(obj2);
                          db.SaveChanges();
                        }
                    }

                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DsrList() 
        {
            var DsrList = _dSRServices.GetDsrList().ToList();
            return View(DsrList);
        }

        public ActionResult VerifyDsr() 
        {
            var data = Db.Sp_VerifyDsr().ToList();

            return View(data);
        }


        public ActionResult Customerddl(int id)
        {
            using (var db = new MealBoxesEntities1())
            {


                var CustomerList = db.Customers_.Where(w => w.areaid == id).Select(s => new
                {

                    CustomerId = s.CustomerID,
                    CustomerName = s.CustomerName,
                   

                }).ToList();

                return Json(CustomerList, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult FinalDsr()
        {
            DSRModel Model = new DSRModel();

            var data = _dSRServices.Finaldsr();
            Model.ViewFinalDsr_ = data;

            return View(Model);

        }
        public ActionResult DsrView(int id) 
        {   
            DSRModel Model = new DSRModel();

            var data = _dSRServices.DsrInvoice(id);
            Model.DsrInvoice = data;

            return View(Model);
        }
        public ActionResult BookingSheet(int id  ,string date) 
        {
            DSRModel Model = new DSRModel();

            var data = _dSRServices.BookingSheet(id, date);
            Model.sP_BookingSheet = data;

            return View(Model);
        }
        public ActionResult VerifyBookingSheet(int id ,string date) 
        {
            DSRModel Model = new DSRModel();

            var data = _dSRServices.VerifyBookingSheet(id, date);
            Model.verifyBooking_Results = data;

            return View(Model);
        }

       

    }
}