using AutoMapper;
using MealBox.Services;
using MealBoxCloud.Infrastructure;
using MealBoxCloud.Models;
using MealBoxCloud.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MealBoxCloud.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        private readonly SaleService _SaleServices;
        private readonly InventoryService _InventoryService;
        private readonly IMapper _mapper;
        public SalesController()
        {
            _SaleServices = new SaleService();
            _mapper = AutoMapperProfile.Mapper;
            _InventoryService = new InventoryService();
        }
        public ActionResult AddSales(int? id)
        {
            using (var db = new MealBoxesEntities())
            {
                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var CustomerList = db.Customers_.ToList();
                ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName");



                var WareHouseList = db.tbl_WareHouse.ToList();
                ViewBag.wareHouseID = new SelectList(WareHouseList, "WarHouseId", "WarHouseName");

                ViewBag.SalNo = db.tbl_MSal.Count() + 1;

                SalesModel Model = new SalesModel();

                if (id > 0)
                {
                    var MsalId = _SaleServices.GetSale(id.Value);
                    ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName", MsalId.CustomerID);
                    Model.MSal_dat = MsalId.MSal_dat;
                    Model.Dis = MsalId.Dis;
                    Model.sdisamt = MsalId.sdisamt;
                    Model.Amt = MsalId.Amt;
                    Model.GTtl = MsalId.GTtl;
                    Model.MSaleid = MsalId.MSal_id;
                    Model.applydiscountamt = MsalId.ApplyDiscAmt.Value;
                    Model.PreBalance = MsalId.PrevBalance;
                    Model.hAmt = MsalId.Amt;
                    Model.ApplyDis = MsalId.ApplyDis;
                    Model.Effected = MsalId.Effected.Value;
                    Model.wareHouseID = MsalId.WareHouseId.Value;
                    Model.saleschilds = db.tbl_DSal.Where(w => w.MSal_id == MsalId.MSal_id).
                    Select(s => new SalesChild
                    {
                        DSalId = s.DSal_id,
                        ItemQty = s.DSal_ItmQty,
                        ItemCost = s.DSal_salcst,
                        ItemName = s.Product.ProductName,
                        ItemTotalAmount = s.Amt,
                        ItemId = s.ProductID,
                        MSalChildID = s.MSal_id
                    }).ToList();

                    return View(Model);
                }

                Model.saleschilds = null;
                Model.Effected = false;
                return View(Model);

            }
        }
        [HttpPost]
        public ActionResult AddSales(SalesModel Model)
        {
            using (var db = new MealBoxesEntities())
            {
                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var CustomerList = db.Customers_.ToList();
                ViewBag.CustomerID = new SelectList(CustomerList, "ProductID", "ProductName");

                ViewBag.SalNo = db.tbl_MSal.Count() + 1;


                var SaleNo = Model.MSal_sono;
                var id = Model.MSaleid;
                var CheakStock = Model.Effected;
                var WareHouse = Model.wareHouseID;

                var CreateSalNo = "Sal";


                if (id == 0)
                {

                    var RanNumString = "007";

                    CreateSalNo = CreateSalNo + RanNumString + "_" + SaleNo;


                    tbl_MSal obj = new tbl_MSal();
                    obj.MSal_dat = Model.MSal_dat;
                    obj.CustomerID = Model.CustomerID;
                    obj.Dis = Model.Dis;
                    obj.Amt = Model.Amt;
                    obj.GTtl = Model.GTtl;
                    obj.Effected = CheakStock;
                    obj.PrevBalance = Model.PreBalance;
                    obj.MSal_sono = CreateSalNo;
                    obj.ApplyDiscAmt = Model.applydiscountamt;
                    obj.ApplyDis = Model.ApplyDis;
                    obj.WareHouseId = Model.wareHouseID;
                    db.tbl_MSal.Add(obj);
                    db.SaveChanges();

                    var EmpolyeeId = _SaleServices.CustomerExist(Model.CustomerID.GetValueOrDefault());

                    if (EmpolyeeId != null)
                    {
                        var SalesCreaditUpdate = EmpolyeeId;
                        SalesCreaditUpdate.CreaditAmount = Model.Amt;
                        _SaleServices.UpdateSalesCreadit(SalesCreaditUpdate);
                    }
                    else
                    {
                        SaleCreadit obj3 = new SaleCreadit();
                        {
                            obj3.CreaditAmount = Model.Amt;
                            obj3.CustomerId = Model.CustomerID;
                            db.SaleCreadits.Add(obj3);
                            db.SaveChanges();
                        }
                    }
                    if (Model.saleschilds != null)
                    {
                        foreach (var item in Model.saleschilds)
                        {
                            tbl_DSal obj2 = new tbl_DSal();
                            {
                                obj2.ProductID = item.ItemId;
                                obj2.DSal_salcst = item.ItemCost;
                                obj2.DSal_ItmQty = item.ItemQty;
                                obj2.Amt = item.ItemTotalAmount;
                                obj2.MSal_id = obj.MSal_id;
                                db.tbl_DSal.Add(obj2);
                                db.SaveChanges();
                            }
                            if (CheakStock.Value)
                            {
                                var a = CheakStock;
                                var Stock = _InventoryService.GetStock(item.ItemId.GetValueOrDefault());
                                Stock.StockQty = Stock.StockQty - item.ItemQty;
                                _InventoryService.UpdateStock(Stock);
                            }

                            if (WareHouse > 0)
                            {
                                var WarehouseStock = _SaleServices.GetWareHouseStock(item.ItemId.GetValueOrDefault());
                                WarehouseStock.Qty = WarehouseStock.Qty - item.ItemQty;
                                _SaleServices.UpdateWareHouseStock(WarehouseStock);
                            }

                        }
                    }
                    var SalCount = db.tbl_MSal.Count() + 1;
                    return Json(SalCount, JsonRequestBehavior.AllowGet);
                }

                var MId = _SaleServices.GetSale(id);
                MId.MSal_dat = Model.MSal_dat;
                MId.Dis = Model.Dis;
                MId.ApplyDiscAmt = Model.applydiscountamt;
                MId.Amt = Model.Amt;
                MId.GTtl = Model.GTtl;
                MId.ApplyDis = Model.ApplyDis;
                var SalNo = MId.MSal_sono;
                var CursalNo = SalNo.Substring(SalNo.IndexOf("_") + 1);

                _SaleServices.UpdateSale(MId);


                if (Model.SaleDele != null)
                {

                    foreach (var itemdel in Model.SaleDele)
                    {
                        var del = db.tbl_DSal.Where(w => w.DSal_id == itemdel.DSaleId).FirstOrDefault();
                        db.tbl_DSal.Remove(del);

                        var Stockupdate = _InventoryService.GetStock(itemdel.ItemDelId);
                        Stockupdate.StockQty = Stockupdate.StockQty + itemdel.ItemDelQty;
                        _InventoryService.UpdateStock(Stockupdate);

                        if (WareHouse > 0)
                        {
                            var WareHouseStockUpdate3 = _SaleServices.GetWareHouseStock(WareHouse.Value, itemdel.ItemDelId);
                            WareHouseStockUpdate3.Qty = WareHouseStockUpdate3.Qty + itemdel.ItemDelQty;
                            _SaleServices.UpdateWareHouseStock(WareHouseStockUpdate3);

                        }

                    }
                    db.SaveChanges();
                }

                if (Model.saleschilds != null)
                {
                    foreach (var item in Model.saleschilds)
                    {
                        var Salstaock = _SaleServices.GetSalStock(id, item.ItemId.Value);


                        if (CheakStock.Value)
                        {
                            var Stockupdate = _InventoryService.GetStock(item.ItemId.GetValueOrDefault());
                            Stockupdate.StockQty = Stockupdate.StockQty + Salstaock.DSal_ItmQty;
                            _InventoryService.UpdateStock(Stockupdate);

                            var Stockupdate2 = _InventoryService.GetStock(item.ItemId.GetValueOrDefault());
                            Stockupdate2.StockQty = Stockupdate.StockQty - item.ItemQty;
                            _InventoryService.UpdateStock(Stockupdate);
                        }

                        if (WareHouse > 0)
                        {
                            var Warehouseupdate = _SaleServices.GetWareHouseStock(WareHouse.Value, item.ItemId.Value);

                            if (Warehouseupdate != null)
                            {
                                Warehouseupdate.Qty = Warehouseupdate.Qty + Salstaock.DSal_ItmQty;
                                _SaleServices.UpdateWareHouseStock(Warehouseupdate);

                                var Warehouseupdate2 = _SaleServices.GetWareHouseStock(WareHouse.Value, item.ItemId.Value);
                                Warehouseupdate2.Qty = Warehouseupdate2.Qty - item.ItemQty;
                                _SaleServices.UpdateWareHouseStock(Warehouseupdate);
                            }


                            tbl_DSal obj2 = new tbl_DSal();
                            var del = db.tbl_DSal.Where(w => w.DSal_id == item.DSalId).FirstOrDefault();

                            if (del != null)
                            {
                                db.tbl_DSal.Remove(del);
                            }
                            obj2.ProductID = item.ItemId;
                            obj2.DSal_salcst = item.ItemCost;
                            obj2.DSal_ItmQty = item.ItemQty;
                            obj2.Amt = item.ItemTotalAmount;
                            obj2.MSal_id = id;
                            db.tbl_DSal.Add(obj2);
                            db.SaveChanges();
                        }
                    }

                }
                return Json(CursalNo, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SalesList()
        {
            var Model = _SaleServices.SaleList();

            return View(Model);
        }

        public ActionResult SaleReturn(int? id)
        {

            SalesModel Model = new SalesModel();

            using (var db = new MealBoxesEntities())
            {

                var ExpensesList = db.Sp_SaleReturn().ToList();
                ViewBag.InvoiceNum = new SelectList(ExpensesList, "MSal_id", "MSal_sono");

                var SaleReturnList = db.tbl_MSal.ToList();
                ViewBag.InvoiceNum = new SelectList(SaleReturnList, "MSal_id", "MSal_sono");

                var AccountsList = db.Accounts.ToList();
                ViewBag.Accno = new SelectList(AccountsList, "AccountID", "AccountName");

            }

            if (id > 0)
            {
                using (var db = new MealBoxesEntities())
                {
                    var MsalId = _SaleServices.GetSale(id.Value);
                    Model.MSal_dat = MsalId.MSal_dat;
                    Model.CustomerID = MsalId.CustomerID;
                    Model.Amt = MsalId.GTtl;
                    var Accno = _SaleServices.GetAccNo(MsalId.CustomerID.Value);
                    if (Accno != null)
                    {

                        Model.Accno = Accno.ToString();

                    }

                    Model.saleschilds = db.tbl_DSal.Where(w => w.MSal_id == id).
                       Select(s => new SalesChild
                       {
                           DSalId = s.DSal_id,
                           ItemQty = s.DSal_ItmQty,
                           ItemCost = s.DSal_salcst,
                           ItemName = s.Product.ProductName,
                           ItemTotalAmount = s.Amt,
                           ItemId = s.ProductID,
                           MSalChildID = s.MSal_id
                       }).ToList();

                    var json = new JavaScriptSerializer().Serialize(Model);
                    return Json(json, JsonRequestBehavior.AllowGet);


                }
            }

            Model.saleschilds = null;

            return View(Model);
        }

        [HttpPost]
        public ActionResult SaleReturn(SalesModel Model)
        {
            using (var db = new MealBoxesEntities())
            {

                var CustomerId = 0;
                var MsalId = 0;
                var Prevbalance = 0.0;
                var NetTotal = 0.0;
                var OpeningBalance = _SaleServices.GetOpeningBalance();
                var Payformula = 0.0;
                var EmployeeId = _SaleServices.GetpersonId(Model.Accno);


                tbl_Salereturn obj = new tbl_Salereturn();
                obj.InvoiceNumber = Model.InvoiceNum;
                obj.ReturnType = Model.ReturnType;
                obj.ReturnTypeId = Model.ReturnTypeId;
                obj.SaleDate = Model.MSal_dat;
                obj.Amount = Model.Amt;
                obj.CreatedBy = 1;
                obj.CreateAt = DateTime.Now;
                db.tbl_Salereturn.Add(obj);
                db.SaveChanges();


                if (Model.saleschilds != null)
                {

                    var SalReturnData = db.tbl_MSal.Where(w => w.MSal_sono == Model.InvoiceNum).FirstOrDefault();

                    var SalPrevBlnc = SalReturnData.PrevBalance;
                    if (SalPrevBlnc == null)
                    {
                        SalPrevBlnc = 0;
                    }
                    var SalAppDisAmt = SalReturnData.ApplyDiscAmt;
                    if (SalPrevBlnc == null)
                    {
                        SalAppDisAmt = 0;
                    }
                    var SalDisAmt = SalReturnData.Dis;

                    if (SalDisAmt == null)
                    {
                        SalDisAmt = 0;
                    }

                    var Gttl = Model.Amt;
                    MsalId = SalReturnData.MSal_id;
                    CustomerId = SalReturnData.CustomerID.Value;
                    var MsalUpdate = _SaleServices.GetSale(MsalId);
                    MsalUpdate.GTtl = Gttl;
                    MsalUpdate.PrevBalance = SalPrevBlnc;
                    MsalUpdate.ApplyDiscAmt = SalAppDisAmt;
                    MsalUpdate.Dis = SalDisAmt;
                    MsalUpdate.Amt = (SalPrevBlnc + Gttl) - (SalAppDisAmt + SalDisAmt);

                    _SaleServices.UpdateSale(MsalUpdate);

                    foreach (var item in Model.saleschilds)
                    {

                        var Salstaock = _SaleServices.GetSalStock(MsalId, item.ItemId.Value);

                        tbl_DSal obj2 = new tbl_DSal();
                        var del = db.tbl_DSal.Where(w => w.DSal_id == item.DSalId).FirstOrDefault();
                        if (del != null)
                        {
                            var Stockup = _InventoryService.GetStock(del.ProductID.Value);
                            Stockup.StockQty = Stockup.StockQty + Salstaock.DSal_ItmQty;
                            _InventoryService.UpdateStock(Stockup);
                            db.tbl_DSal.Remove(del);
                        }

                        obj2.ProductID = item.ItemId;
                        obj2.DSal_salcst = item.ItemCost;
                        obj2.DSal_ItmQty = item.ItemQty;
                        obj2.Amt = item.ItemTotalAmount;
                        obj2.MSal_id = MsalId;
                        db.tbl_DSal.Add(obj2);
                        db.SaveChanges();

                        tbl_SalereturnChild obj6 = new tbl_SalereturnChild();
                        obj6.ProductId = item.ItemId;
                        obj6.productCost = item.ItemCost;
                        obj6.ProductAmt = item.ItemTotalAmount;
                        obj6.ProductQty = item.ItemQty;
                        db.tbl_SalereturnChild.Add(obj6);
                        db.SaveChanges();

                        var Stock = _InventoryService.GetStock(item.ItemId.Value);
                        Stock.StockQty = Stock.StockQty - item.ItemQty;
                        _InventoryService.UpdateStock(Stock);

                    }
                }

                tbl_expenses obj3 = new tbl_expenses();
                obj3.expensesdat = Model.MSal_dat;
                obj3.typeofpay = "Cash";
                obj3.Amountpaid = Model.Amt;
                obj3.accno = Model.Accno;
                obj3.PaymentOut = Model.Amt;
                obj3.PaymentIn = 0;
                obj3.billno = Model.InvoiceNum;
                db.tbl_expenses.Add(obj3);
                db.SaveChanges();


                Payformula = (OpeningBalance + 0) - NetTotal;
                tbl_CB obj4 = new tbl_CB();
                obj4.CB_dat = DateTime.Now;
                obj4.CBPartiID = EmployeeId;
                obj4.CBDBAmt = Model.Amt.Value;
                obj4.CBCrAmt = 0;
                obj4.AmountVar = Payformula;
                obj4.CBPartiID = EmployeeId;
                db.tbl_CB.Add(obj4);
                db.SaveChanges();

                var Customer = _SaleServices.CustomerExist(CustomerId);

                if (Customer != null)
                {
                    var SalesCreaditUpdate = Customer;
                    SalesCreaditUpdate.CreaditAmount = SalesCreaditUpdate.CreaditAmount + Model.Amt.Value;
                    _SaleServices.UpdateSalesCreadit(SalesCreaditUpdate);
                }


                return Json("Success", JsonRequestBehavior.AllowGet);
            }

            return View();

        }
        public ActionResult Accountddl()
        {
            using (var db = new MealBoxesEntities())
            {
                var AccList = db.Accounts.Select(s => new
                {
                    AccNo = s.AccountGeneratedCodeId,
                    AccName = s.AccountName,
                    EmpId = s.PersonId

                }).ToList();

                return Json(AccList, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CustomerView()
        {

            return PartialView("_ViewBill");
        }

        public ActionResult BillView()
        {

            SalesModel model = new SalesModel();

            return PartialView("_ViewBill", model);
        }

        [HttpPost]
        public ActionResult Hold(SalesModel model)
        {
            using (var db = new MealBoxesEntities())
            {
                //tbl_HoldMPso obj = new tbl_HoldMPso();
                //obj.BillNo = model.BillNo;
                //obj.CustomerName = model.CustomerName;
                //obj.CellNo = model.CellNo1;
                //obj.BiLLdate = model.BiilDate;
                //obj.AmountPaid = model.AmountPaid;
                //obj.Balance = model.Balance;
                //obj.DiscountAmount = model.DiscountAmount;
                //obj.TotalAmount = model.TotalAmount;
                //obj.C_17Per = model.C17Per;
                //obj.C_3Per = model.C3Per;
                //db.tbl_HoldMPso.Add(obj);
                //db.SaveChanges();

                if (model.saleschilds != null)
                {
                    foreach (var item in model.saleschilds)
                    {
                        //tbl_HoldDPso obj2 = new tbl_HoldDPso();
                        //obj2.ProductId = item.ItemId;
                        //obj2.ProductQuntity = item.ItemQty;
                        //obj2.TotalAmount = item.ItemTotalAmount;
                        //obj2.ProductPrice = item.ItemCost;
                        //obj2.HoldMposid = obj.HoldMposid;
                        //db.tbl_HoldDPso.Add(obj2);
                        //db.SaveChanges();
                    }
                }
            }

            return Json("success", JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetSaleCredit(int id)
        {
            var Data = _SaleServices.SAleCreadit(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleInvoice(int id)
        {
            SalesModel Model = new SalesModel();
            var Data = _SaleServices.GetSaleInvoice(id);
            Model.SaleInvoice = Data;
            return View(Model);
        }




    }
}