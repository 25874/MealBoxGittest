using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBox.Services;
using MealBox.Models;

using AutoMapper;
using MealBoxes.Infrastructure;

namespace MealBox.Controllers
{
    public class AccountController : Controller
    {
        MealBoxesEntities db = new MealBoxesEntities();

        private readonly IMapper _mapper;
        private readonly AccountService AccountService;
        // GET: Account

        public AccountController()
        {
            AccountService = new AccountService();
            _mapper = AutoMapperProfile.Mapper;
        }
        public ActionResult Index(int? Id, string Type)
        {
            using (var db = new MealBoxesEntities())
            {

                ViewBag.SubAccountId = new SelectList("");

                AccountModel ModelList = new AccountModel();

                var AccountList = AccountService.GetHeadsList();

                //var 
                var Headdata = _mapper.Map<List<AccountHeadMOdel>>(AccountList);

                ViewBag.AccounId = new SelectList(AccountList, "HeadGeneratedIdCode", "HeadName");

                ModelList.accountHeads = Headdata;

                var SubHeadList = AccountService.SubHeadtbl();


                ModelList.SubHeads = SubHeadList;

                var Accounttbl = AccountService.Accounttbl();

                ModelList.Accounttbl = Accounttbl;

                return View(ModelList);
            }

            return View();

        }

        [HttpPost]
        public ActionResult Index(AccountModel Model)
        {
            var msg = "";
            var Type = Model.Type;

            if (Type == "Subhead")
            {
                AccountService.AddSubHead(Model);

                msg = "Subhead";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else if(Type == "Account") 
            {
                AccountService.AddAccount(Model);

                msg = "Account";

                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SubAccountBody()
        {
            var AccountList = AccountService.SubHeadtbl();

            var SubHeaddata = _mapper.Map<List<SubHeadModel>>(AccountList);

            return PartialView("_SubHead", SubHeaddata);
        }

        public ActionResult AccountBody()
        {

            var AccountList = AccountService.Accounttbl();

            var Accountdata = _mapper.Map<List<SubHeadCategoriesModel>>(AccountList);

            return PartialView("_SubHeadAccount", Accountdata);
        }

        public ActionResult HeadDDl()
        {
            var HeadList = AccountService.GetHeadsList();
            
            return Json(HeadList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SubHeadDDl(string id)
        {
        
         var SubHeeadDDl = AccountService.SubHeadDDl(id);
        
         return Json(SubHeeadDDl, JsonRequestBehavior.AllowGet);
        
        }

           public ActionResult Transaction() 
          {

            ViewBag.PayAccount = new  SelectList( "", "" );

            ViewBag.RecAccount = new SelectList( "", "");

            return View();
          }

        [HttpPost]
        public ActionResult Transaction(Transactionmodel Model)
        {
            using(var db = new MealBoxesEntities()) 
            {
                var payType = Model.type;                
                var Empid = Model.EmployeeId;
                var openingBalance = Model.opening_balance;
                var Payformula = 0.0;

                tbl_expenses obj = new tbl_expenses();
                obj.expensesdat = Model.expensesdat;
                obj.typeofpay = Model.typeofpay;
                obj.Amountpaid = Model.Amountpaid;
                obj.prevbal = Model.prevbal;
                obj.accno = Model.accno;
                if(payType == "Pay")
                {
                    obj.PaymentOut = Model.Amountpaid;
                    obj.PaymentIn = 0;
                }
                else
                {
                    obj.PaymentOut = 0;
                    obj.PaymentIn = Model.Amountpaid;
                }                
                db.tbl_expenses.Add(obj);
                db.SaveChanges();
               
                if(payType == "Pay") 
                {
                    Payformula = (openingBalance.Value + 0) - Model.Amountpaid.Value;
                    tbl_CB obj2 = new tbl_CB();
                    obj2.CB_dat = Model.expensesdat;
                    obj2.CBPartiID = Empid;
                    obj2.CBDBAmt = Model.Amountpaid;
                    obj2.CBCrAmt = 0;                    
                    obj2.AmountVar = Payformula;
                    db.tbl_CB.Add(obj2);
                    db.SaveChanges();
                    if (Model.prevbal != null)
                    {
                        var SupexistId = AccountService.SupllierExist(Empid.ToString());
                        if (SupexistId != null)
                        {
                            SupexistId.CredAmt = SupexistId.CredAmt - Model.Amountpaid;
                            AccountService.UpdatePurchaseCreadit(SupexistId);
                        }   
                    }
                }

                if (payType == "Rec")
                {

                    Payformula = (openingBalance.Value + Model.Amountpaid.Value) - 0;
                    tbl_CB obj2 = new tbl_CB();
                    obj2.CB_dat = Model.expensesdat;
                    obj2.CBPartiID = Empid;
                    obj2.CBDBAmt = 0;
                    obj2.CBCrAmt = Model.Amountpaid;
                    obj2.AmountVar = Payformula;
                    db.tbl_CB.Add(obj2);
                    db.SaveChanges();
                    if(Model.prevbal != null)
                    {
                        var CusexistId = AccountService.CustomerExist(Empid);
                        if(CusexistId != null)
                        {
                            CusexistId.CreaditAmount = CusexistId.CreaditAmount - Model.Amountpaid;
                            AccountService.UpdateSalesCreadit(CusexistId);                        
                        }
                    }
                }
 
                return Json(Payformula.ToString(), JsonRequestBehavior.AllowGet);
            }
               
        }

        public ActionResult PayAccouvtddl() 
        {
            var PayList = db.Accounts.Where(w => w.headGeneratedIdCode == "003" && w.AccountName != null).Select(s => new
            {

                AccNo = s.AccountGeneratedCodeId,
                AccName = s.AccountName,
                EmpId = s.PersonId

            }).ToList();
            
            return Json(PayList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecAccouvtddl()
        {
            var RecvList = db.Accounts.Where(w => w.headGeneratedIdCode == "001" && w.AccountName != null).Select(s => new
            {
                AccNo = s.AccountGeneratedCodeId,
                AccName = s.AccountName,
                EmpId = s.PersonId

            }).ToList();

            return Json(RecvList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpeningBalance() 
        {
           
            var openbala = AccountService.GetOpeningBalance();
            
            return Json(openbala, JsonRequestBehavior.AllowGet);

        }

    }
}