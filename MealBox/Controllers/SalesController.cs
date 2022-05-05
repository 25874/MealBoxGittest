using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MealBoxes.Infrastructure;
using MealBox.Services;
using MealBox.Models;
using System.Web.Script.Serialization;


namespace MealBox.Controllers
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
            using(var db = new MealBoxesEntities())
            {
                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var CustomerList = db.Customers_.ToList();
                ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName");

                ViewBag.SalNo = db.tbl_MSal.Count() + 1;

                SalesModel Model = new SalesModel();
              
                if(id > 0) 
                {
                    var MsalId = _SaleServices.GetSale(id.Value); 
                    ViewBag.CustomerID = new SelectList(CustomerList, "CustomerID", "CustomerName", MsalId.CustomerID);
                    Model.MSal_dat = MsalId.MSal_dat;
                    Model.Dis = MsalId.Dis;
                    Model.sdisamt = MsalId.sdisamt;
                    Model.Amt = MsalId.Amt;
                    Model.GTtl = MsalId.GTtl;
                    Model.MSaleid = MsalId.MSal_id;
                    Model.saleschilds = db.tbl_DSal.Where(w => w.MSal_id == MsalId.MSal_id).                                              
                        Select(s => new SalesChild                
                          {
                              DSalId = s.DSal_id,
                              ItemQty = s.DSal_ItmQty,
                              ItemCost = s.DSal_ItmQty,
                              ItemName = s.Product.ProductName,
                              ItemTotalAmount = s.Amt,
                              ItemId = s.ProductID,
                              MSalChildID = s.MSal_id
                          }).ToList();
                   
                    return View(Model);
                }

                Model.saleschilds = null;                
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

                var id = Model.MSaleid;
                var CheakStock = Model.Effected;

                if (id == 0)
                {
                    tbl_MSal obj = new tbl_MSal();
                    obj.MSal_dat = Model.MSal_dat;
                    obj.CustomerID = Model.CustomerID;
                    obj.Dis = Model.Dis;
                    obj.Amt = Model.Amt;
                    obj.GTtl = Model.GTtl;
                    obj.Effected = CheakStock;
                    obj.PrevBalance = Model.PreBalance; 
                    obj.MSal_sono = Model.MSal_sono;

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
                    if(Model.saleschilds != null)
                    {
                        foreach (var item in Model.saleschilds)
                        {
                            tbl_DSal obj2 = new tbl_DSal();
                            {
                                obj2.ProductID =   item.ItemId;
                                obj2.DSal_salcst = item.ItemCost;
                                obj2.DSal_ItmQty = item.ItemQty;
                                obj2.Amt =          item.ItemTotalAmount;
                                obj2.MSal_id = obj.MSal_id;
                                db.tbl_DSal.Add(obj2);
                                db.SaveChanges();
                            }
                            if(CheakStock.Value)
                            {
                                var a = CheakStock;
                                var Stock = _InventoryService.GetStock(item.ItemId.GetValueOrDefault());
                                Stock.StockQty = Stock.StockQty - item.ItemQty;
                                _InventoryService.UpdateStock(Stock);
                            }
                        }
                    }                    
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
               
                var MId = _SaleServices.GetSale(id);
                MId.MSal_dat = Model.MSal_dat;
                MId.Dis = Model.Dis;
                MId.sdisamt = Model.sdisamt;
                MId.Amt = Model.Amt;
                MId.GTtl = Model.GTtl;
                db.SaveChanges();

                if (Model.SaleDele != null)
                {
                    foreach (var itemdel in Model.SaleDele)
                    {
                        var del = db.tbl_DSal.Where(w => w.DSal_id == itemdel.DSaleId).FirstOrDefault();
                        db.tbl_DSal.Remove(del);
                    }
                    db.SaveChanges();
                }

                if(Model.saleschilds != null)
                {
                    foreach (var item in Model.saleschilds)
                    {
                        tbl_DSal obj2 = new tbl_DSal();
                        var del = db.tbl_DSal.Where(w => w.DSal_id == item.DSalId).FirstOrDefault();

                        if (del != null)
                        {
                            db.tbl_DSal.Remove(del);
                        }
                        obj2.ProductID = item.ItemId;
                        obj2.DSal_salcst = item.ItemCost;
                        obj2. DSal_ItmQty = item.ItemQty;
                        obj2.Amt= item.ItemTotalAmount;
                        obj2.MSal_id = id;
                        db.tbl_DSal.Add(obj2);
                        db.SaveChanges();   
                    }
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SalesList()
        {
            var Model = _SaleServices.SaleList();
            
            return View(Model);
        }

        public ActionResult Pos() 
        {
            using(var db = new MealBoxesEntities())
            {
                var ProductList = db.Products.ToList();
                ViewBag.ItemCode = new SelectList(ProductList, "ProductID", "ProductCode");

                var ProductList2 = db.Products.ToList();
                ViewBag.ItemName = new SelectList(ProductList2, "ProductID", "ProductName");

                PosModel model = new PosModel();

                var PosList = _SaleServices.GetPos();

                var  posdata  =   _mapper.Map<List<Postable>>(PosList);

                model.postables = posdata;

                ViewBag.BillNO = db.Mpos.Count() + 1  ;

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Pos(PosModel Model) 
        {
            using (var db = new MealBoxesEntities())
            {
                Mpos obj = new Mpos();
                obj.BillNo = Model.BillNo;
                obj.CustomerName = Model.CustomerName;
                obj.CellNo = Model.CellNo;
                obj.BiLLdate = DateTime.Now;
                obj.AmountPaid = Model.AmountPaid;
                obj.Balance = Model.Balance;
                obj.DiscountAmount = Model.DiscountAmount;
                obj.TotalAmount = Model.TotalAmount;
                obj.C_17Per = Model.C17Per;
                obj.C_3Per = Model.C3Per;
                obj.Amount = Model.Amount;
                obj.Per17Amt = Model.per7amt;
                obj.Per3Amt = Model.per3amt; 
                db.Mpos.Add(obj);
                db.SaveChanges();

                var OpeningBalance = _SaleServices.GetOpeningBalance();
                var Payformula = 0.0;

                if (Model.posChildren != null)
                {
                    foreach (var item in Model.posChildren)
                    {
                        Dpos obj2 = new Dpos();
                        obj2.Productid = item.ItemId;
                        obj2.ProductQuantity = item.ItemQty;
                        obj2.ProductAmount = item.ItemTotalAmount;
                        obj2.productPrice = item.ItemCost;
                        obj2.Mposid = obj.Mposid;
                        db.Dpos.Add(obj2);
                        db.SaveChanges();

                        var Stock = _InventoryService.GetStock(item.ItemId.GetValueOrDefault());
                        Stock.StockQty = Stock.StockQty - item.ItemQty;
                        _InventoryService.UpdateStock(Stock);
                       
                    }
                    
                    tbl_expenses obj4 = new tbl_expenses();
                    obj4.expensesdat = DateTime.Now;
                    obj4.typeofpay = "Cash";
                    obj4.Amountpaid = Model.AmountPaid;
                    obj4.accno = Model.Accno;
                    obj4.PaymentOut = 0;
                    obj4.billno = Model.BillNo;
                    obj4.PaymentIn = Model.AmountPaid;
                    db.tbl_expenses.Add(obj4);
                    db.SaveChanges();


                    Payformula = (OpeningBalance + Model.AmountPaid.Value) - 0;
                    tbl_CB obj3 = new tbl_CB();
                    obj3.CB_dat = DateTime.Now;
                    obj3.CBPartiID = 9;
                    obj3.CBDBAmt = 0;
                    obj3.CBCrAmt = Model.AmountPaid.Value;
                    obj3.AmountVar = Payformula;
                    obj3.CBPartiID = Model.EmployeeId;
                    db.tbl_CB.Add(obj3);
                    db.SaveChanges();
                    
                    return Json("Success", JsonRequestBehavior.AllowGet);
                
                }
                
            }

            return View();
        }

        public ActionResult SaleReturn(int? id)
        {

            SalesModel Model = new SalesModel();

            using (var db = new MealBoxesEntities())
            {
             
                var ProductList = db.Products.ToList();
                ViewBag.ProductID = new SelectList(ProductList, "ProductID", "ProductName");

                var SaleReturnList = db.tbl_MSal.ToList();
                ViewBag.InvoiceNum = new SelectList(SaleReturnList, "MSal_id", "MSal_sono");

                var AccountsList = db.Accounts.ToList();
                ViewBag.Accno = new SelectList(AccountsList, "AccountGeneratedCodeId", "AccountName");


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
                    if(Accno != null) 
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
                    tbl_MSal obj7 = new tbl_MSal();

                    var SalReturn = db.tbl_MSal.Where(w => w.MSal_sono == Model.InvoiceNum).FirstOrDefault();
                    SalReturn.MSal_dat = SalReturn.MSal_dat;
                    SalReturn.Dis = SalReturn.Dis;
                    SalReturn.GTtl = Model.Amt;
                    SalReturn.PrevBalance = SalReturn.PrevBalance;
                    SalReturn.Amt = (Model.Amt + SalReturn.PrevBalance) - SalReturn.Dis;
                    CustomerId = SalReturn.CustomerID.Value;
                    MsalId = SalReturn.MSal_id;
                    db.SaveChanges();
                    var Res  = (Model.Amt + SalReturn.PrevBalance) - SalReturn.Dis;
                    NetTotal = Res.Value;
                    Prevbalance = SalReturn.PrevBalance.Value;
                  
                    foreach (var item in Model.saleschilds)
                    {
                        tbl_DSal obj2 = new tbl_DSal();
                        var del = db.tbl_DSal.Where(w => w.DSal_id == item.DSalId).FirstOrDefault();

                        if (del != null)
                        {
                            var Stockup = _InventoryService.GetStock(del.ProductID.Value);
                            Stockup.StockQty = Stockup.StockQty + del.DSal_ItmQty;
                            _InventoryService.UpdateStock(Stockup);
                            db.tbl_DSal.Remove(del);

                        }
                        obj2.ProductID = item.ItemId;
                        obj2.DSal_salcst = item.ItemCost;
                        obj2.DSal_ItmQty = item.ItemQty;
                        obj2.Amt = item.ItemTotalAmount;
                        obj2.MSal_id = del.MSal_id;
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

                //tbl_expenses obj3 = new tbl_expenses();
                //obj3.expensesdat = Model.MSal_dat;
                //obj3.typeofpay = "Cash";
                //obj3.Amountpaid = NetTotal;
                //obj3.accno = Model.Accno;
                //obj3.PaymentOut = NetTotal;
                //obj3.PaymentIn = 0;
                //obj3.billno = Model.InvoiceNum;
                //db.tbl_expenses.Add(obj3);
                //db.SaveChanges();


                //Payformula = (OpeningBalance + 0) - NetTotal;
                //tbl_CB obj4 = new tbl_CB();
                //obj4.CB_dat = DateTime.Now;
                //obj4.CBPartiID = EmployeeId;
                //obj4.CBDBAmt = Model.Amt.Value;
                //obj4.CBCrAmt = 0;
                //obj4.AmountVar = Payformula;
                //obj4.CBPartiID = EmployeeId;
                //db.tbl_CB.Add(obj4);
                //db.SaveChanges();

                //tbl_expenses obj8 = new tbl_expenses();
                //obj8.expensesdat = Model.MSal_dat;
                //obj8.typeofpay = "Cash";
                //obj8.Amountpaid = Model.Amt;
                //obj8.accno = Model.Accno;
                //obj8.PaymentOut = 0;
                //obj8.PaymentIn = Model.Amt;
                //obj8.billno = Model.InvoiceNum;
                //db.tbl_expenses.Add(obj3);
                //db.SaveChanges();


                //Payformula = (OpeningBalance + Model.Amt.Value) - 0;
                //tbl_CB obj9 = new tbl_CB();
                //obj9.CB_dat = DateTime.Now;
                //obj9.CBPartiID = EmployeeId;
                //obj9.CBDBAmt = Model.Amt.Value;
                //obj9.CBCrAmt = 0;
                //obj9.AmountVar = Payformula;
                //obj9.CBPartiID = EmployeeId;
                //db.tbl_CB.Add(obj9);
                //db.SaveChanges();

                var Customer = _SaleServices.CustomerExist(CustomerId);

                if (Customer != null)
                {
                    var SalesCreaditUpdate = Customer;
                    SalesCreaditUpdate.CreaditAmount = Prevbalance + NetTotal;
                    _SaleServices.UpdateSalesCreadit(SalesCreaditUpdate);
                }
                else
                {
                    SaleCreadit obj5 = new SaleCreadit();
                    {
                        obj5.CreaditAmount = -Model.Amt;
                        obj5.CustomerId = Model.CustomerID;
                        db.SaleCreadits.Add(obj5);
                        db.SaveChanges();
                    }
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
                    foreach(var item in model.saleschilds) 
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

            return Json("success",JsonRequestBehavior.AllowGet);

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

        public ActionResult PosBody()
        {

            var PosList = _SaleServices.GetPos();

            var posdata = _mapper.Map<List<Postable>>(PosList);

            return PartialView("_PostRen", posdata);

        }

        public ActionResult Testing() 
        {
            return View();
        }

        public ActionResult PosInvoice(int? id) 
        {
            using(var db = new MealBoxesEntities())
            {
                PosModel model = new PosModel();

                var data = db.Sp_PosInvoice(id).ToList();

                model.sp_PosInvoice = data;

                return View(model);

            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
        }
        public ActionResult WalkinCutomer() 
        {
            var data = _SaleServices.GetWalkin();

            return Json(data , JsonRequestBehavior.AllowGet);        
        }
    }
}