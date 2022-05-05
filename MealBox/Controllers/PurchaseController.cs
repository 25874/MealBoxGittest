using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBoxes.Infrastructure;
using MealBoxes.Services;
using AutoMapper;
using MealBox.Services;
using MealBox.Models;
using System.Web.Script.Serialization;

namespace MealBox.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase

        private readonly PurchaseService _PurchaseServices;

        private readonly IMapper _mapper;
        public PurchaseController()
        {
            _PurchaseServices = new PurchaseService();
            _mapper = AutoMapperProfile.Mapper;
        }

        public ActionResult AddPurchase(int? id)
        {
            using (var db = new MealBoxesEntities())
            {
                var VenderList = db.suppliers.ToList();
                ViewBag.ven_id = new SelectList(VenderList, "supplierId", "suppliername");

                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var BankList = db.tbl_CashBnk.ToList();
                ViewBag.BankId = new SelectList(BankList, "CashBnk_id", "CashBnk_nam");


                ViewBag.PurNo = 1 + db.MPurchases.Count();

                PurchaseModel model = new PurchaseModel();

                if (id > 0)
                {
                    var MId = _PurchaseServices.GetPurchase(id.Value);
                    var PaymentTyplist = db.tbl_PayTyp.ToList();
                    var VchTyplist = db.tbl_vchtyp.ToList();

                    ViewBag.ven_id = new SelectList(VenderList, "supplierId", "suppliername", MId.ven_id);
                    model.MPurDate = MId.MPurDate;
                    model.GrossTtal = MId.NetTotal;
                    model.PayTyp_id = MId.PayTyp_id;
                    model.vchtyp_id = MId.vchtyp_id;
                    model.MPurID = id.Value;
                    model.Effected = MId.Effected;
                    model.NetTotal = MId.NetTotal;
                    model.GrossTtal = MId.GrossTtal;
                    model.ItemChild = db.DPurchases.Where(w => w.MPurID == MId.MPurID).
                        Select(s => new ItemChild
                        {
                            DpurId = s.DPurID,
                            ItemQty = s.Qty,
                            ItemCost = s.Cost,
                            ItemName = s.Product1.ProductName,
                            ItemTotalAmount = s.Amount,
                            ItemId = s.ProductID,
                            MPurChildID = s.MPurID
                        }).ToList();

                    return View(model);
                }

                model.ItemChild = null;

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult AddPurchase(PurchaseModel Model)
        {
            using (var db = new MealBoxesEntities())
            {

                var VenderList = db.suppliers.ToList();
                ViewBag.ven_id = new SelectList(VenderList, "supplierId", "suppliername");

                var BankList = db.tbl_CashBnk.ToList();
                ViewBag.BankId = new SelectList(BankList, "CashBnk_id", "CashBnk_nam");

                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                ViewBag.PurNo = 1 + db.MPurchases.Count();

                var id = Model.MPurID;
                var CheakStock = Model.Effected;

                if (id == 0)
                {

                    MPurchase obj = new MPurchase();
                    {
                        obj.MPurDate = Model.MPurDate;
                        obj.vchtyp_id = Model.vchtyp_id;
                        obj.PayTyp_id = Model.PayTyp_id;
                        obj.csh_amt = Model.csh_amt;
                        obj.ven_id = Model.ven_id;
                        obj.NetTotal = Model.NetTotal;
                        obj.GrossTtal = Model.GrossTtal;
                        obj.PurNo = Model.PurNo;
                        obj.Effected = CheakStock;
                        obj.CashBnk_id = Model.CashBnk_id;
                        obj.chque_No = Model.chque_No;
                        obj.Discount = Model.Discount;
                        obj.PrevBal = Model.PreviousBalnace;
                        db.MPurchases.Add(obj);
                        db.SaveChanges();
                    }

                    var EmpolyeeId = _PurchaseServices.SupllierExist(Model.ven_id.ToString());

                    if (EmpolyeeId != null)
                    {
                        var PurchaseCreaditUpdate = EmpolyeeId;
                        PurchaseCreaditUpdate.CredAmt = Model.NetTotal;
                        _PurchaseServices.UpdatePurchaseCreadit(PurchaseCreaditUpdate);
                    }
                    else
                    {
                        tbl_Purcredit obj3 = new tbl_Purcredit();
                        {
                            obj3.CredAmt = Model.NetTotal;
                            obj3.supplierId = Convert.ToString(Model.ven_id.GetValueOrDefault());
                            db.tbl_Purcredit.Add(obj3);
                            db.SaveChanges();
                        }
                    }


                    if(Model.ItemChild != null)
                    {
                        foreach (var item in Model.ItemChild)
                        {
                            DPurchase obj2 = new DPurchase();
                            {
                                obj2.ProductID = item.ItemId;
                                obj2.Cost = item.ItemCost;
                                obj2.Qty = item.ItemQty;
                                obj2.Amount = item.ItemTotalAmount;
                                obj2.MPurID = obj.MPurID;
                                db.DPurchases.Add(obj2);
                                db.SaveChanges();
                            }
                            if (CheakStock.Value)
                            {
                                var StockUpdate = _PurchaseServices.GetStock(item.ItemId.GetValueOrDefault());
                                StockUpdate.StockQty = StockUpdate.StockQty + item.ItemQty;
                                StockUpdate.unitprice = item.ItemCost;
                                _PurchaseServices.UpdateStock(StockUpdate);
                            }
                        }
                    }
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }

                var MId = _PurchaseServices.GetPurchase(id);
                MId.MPurDate = Model.MPurDate;
                MId.GrossTtal = Model.NetTotal;
                MId.PayTyp_id = Model.PayTyp_id;
                db.SaveChanges();

                if (Model.deleteItems != null)
                {
                    foreach (var itemdel in Model.deleteItems)
                    {
                        var del = db.DPurchases.Where(w => w.DPurID == itemdel.DpurId).FirstOrDefault();
                        db.DPurchases.Remove(del);
                    }
                    db.SaveChanges();
                }

                if (Model.ItemChild != null)
                {
                    foreach (var item in Model.ItemChild)
                    {
                        DPurchase obj2 = new DPurchase();
                        var del = db.DPurchases.Where(w => w.DPurID == item.DpurId).FirstOrDefault();
                        if (del != null)
                        {
                            db.DPurchases.Remove(del);
                        }
                        obj2.ProductID = item.ItemId;
                        obj2.Cost = item.ItemCost;
                        obj2.Qty = item.ItemQty;
                        obj2.Amount = item.ItemTotalAmount;
                        obj2.MPurID = id;
                        db.DPurchases.Add(obj2);
                        db.SaveChanges();
                        if(CheakStock.Value)
                        {
                            var StockUpdate = _PurchaseServices.GetStock(item.ItemId.GetValueOrDefault());
                            StockUpdate.StockQty = 0;
                            StockUpdate.StockQty = StockUpdate.StockQty + item.ItemQty;
                            _PurchaseServices.UpdateStock(StockUpdate);
                        }
                    }
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PurchaseList()
        {
            var Model = _PurchaseServices.PurchaseList();
            return View(Model);
        }

        public ActionResult GetCredit(string id)
        {
            var Data = _PurchaseServices.GetCreadit(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseInvoice(int id)
        {
            PurchaseModel Model = new PurchaseModel();

            var data = _PurchaseServices.PurchaseInvoice(id);
            Model.PurchaseInvoice = data;

            return View(Model);
        }

        public ActionResult PurchaseReturn(int? id)
        {

            PurchaseModel Model = new PurchaseModel();

            using (var db = new MealBoxesEntities())
            {
                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var AccountsList = db.Accounts.ToList();
                ViewBag.Accno = new SelectList(AccountsList, "AccountGeneratedCodeId", "AccountName");

                var PurchaseList = db.MPurchases.ToList();
                ViewBag.InvoiceNum = new SelectList(PurchaseList, "MPurID", "PurNo");

            }

            if (id > 0)
            {
                using (var db = new MealBoxesEntities())
                {
                    var MsalId = _PurchaseServices.GetPurchase(id.Value);
                    Model.MPurDate = MsalId.MPurDate;
                    Model.ven_id = MsalId.ven_id;
                    Model.Amount = MsalId.GrossTtal;
                    var Accno = _PurchaseServices.GetAccNo(MsalId.ven_id.Value);

                    if (Accno != null)
                    {
                        Model.Accno = Accno.ToString();
                    }

                    Model.ItemChild = db.DPurchases.Where(w => w.MPurID == id).

                       Select(s => new ItemChild
                       {
                           DpurId = s.DPurID,
                           ItemQty = s.Qty,
                           ItemCost = s.Cost,
                           ItemName = s.Product1.ProductName,
                           ItemTotalAmount = s.Amount,
                           ItemId = s.ProductID,
                           MPurChildID = s.MPurID
                       }).ToList();

                    var json = new JavaScriptSerializer().Serialize(Model);

                    return Json(json, JsonRequestBehavior.AllowGet);

                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult PurchaseReturn(PurchaseModel Model)
        {
            using (var db = new MealBoxesEntities())
            {

                var OpeningBalance = _PurchaseServices.GetOpeningBalance();
                var Payformula = 0.0;
                var EmployeeId = _PurchaseServices.GetpersonId(Model.Accno);


                //obj.InvoiceNumber = Model.InvoiceNum;
                //obj.ReturnType = Model.ReturnType;
                //obj.ReturnTypeId = Model.ReturnTypeId;
                //obj.SaleDate = Model.MPurDate;
                //obj.Amount = Model.PurchaseTotal;
                //obj.CreatedBy = 1;
                //obj.CreateAt = DateTime.Now;
                //db.tbl_PurchaseReturn.Add(obj);

                tbl_expenses obj3 = new tbl_expenses();
                obj3.expensesdat = Model.MPurDate;
                obj3.typeofpay = "Cash";
                obj3.Amountpaid = Model.PurchaseTotal;
                obj3.accno = Model.Accno;
                obj3.PaymentOut = Model.PurchaseTotal;
                obj3.PaymentIn = 0;
                obj3.billno = Model.InvoiceNum;
                db.tbl_expenses.Add(obj3);
                db.SaveChanges();

                Payformula = (OpeningBalance + Model.PurchaseTotal.Value) - 0;
                tbl_CB obj2 = new tbl_CB();
                obj2.CB_dat = Model.MPurDate;
                obj2.CBPartiID = EmployeeId;
                obj2.CBDBAmt = 0;
                obj2.CBCrAmt = Model.PurchaseTotal;
                obj2.AmountVar = Payformula;
                db.tbl_CB.Add(obj2);
                db.SaveChanges();

                var EmpolyeeId = _PurchaseServices.SupllierExist(Model.ven_id.ToString());

                if (EmpolyeeId != null)
                {
                    var PurchaseCreaditUpdate = EmpolyeeId;
                    PurchaseCreaditUpdate.CredAmt = Model.NetTotal;
                    _PurchaseServices.UpdatePurchaseCreadit(PurchaseCreaditUpdate);
                }
                else
                {
                    tbl_Purcredit obj4 = new tbl_Purcredit();
                    {
                        obj4.CredAmt = Model.NetTotal;
                        obj4.supplierId = Convert.ToString(Model.ven_id.GetValueOrDefault());
                        db.tbl_Purcredit.Add(obj4);
                        db.SaveChanges();
                    }
                }

                if (Model.ItemChild != null)
                {
                    foreach (var item in Model.ItemChild)
                    {
                        //tbl_PurchaseReturn obj5 = new tbl_PurchaseReturn();
                        //obj5. = item.ItemId;
                        //obj5.productCost = item.ItemCost;
                        //obj5.ProductAmt = item.ItemTotalAmount;
                        //obj5.ProductQty = item.ItemQty;
                        //obj5.SaleReturnId = obj.SaleReturnId;
                        //db.tbl_SalereturnChild.Add(obj2);
                        //db.SaveChanges();

                        //var Stock = _InventoryService.GetStock(item.ItemId);
                        //Stock.StockQty = Stock.StockQty + item.ItemQty;
                        //Stock.unitprice = item.ItemCost;
                        //_InventoryService.UpdateStock(Stock);

                    }
                }
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
            return View();
        }

    }
}