using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBox.Services;
using MealBoxCloud.Models;
using AutoMapper;
using MealBoxCloud.Infrastructure;
using MealBoxCloud.Services;

namespace MealBoxCloud.Controllers
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

            ViewBag.PayAccount = new  SelectList("", "");

            ViewBag.RecAccount = new SelectList("", "");

            var BankList = db.tbl_CashBnk.ToList();
            ViewBag.CashBnk_id = new SelectList(BankList , "CashBnk_id", "CashBnk_nam");

            ViewBag.SaleReturn =   new SelectList("", "");
            ViewBag.PurchaseReturn = new SelectList("", "");

            return View();
          }

        [HttpPost]
        
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

        public ActionResult Saleddl()
        {
            var SaleList = db.tbl_MSal.Select(s => new
            {
                SaleId = s.MSal_id,
                SaleNo = s.MSal_sono
            }).ToList();

            return Json(SaleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Purddl()
        {
            var SaleList = db.MPurchases.Select(s => new
            {
                PurchaseId = s.MPurID,
                PurchaseNo = s.PurNo
            }).ToList();

            return Json(SaleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpeningBalance() 
        {
           
            var openbala = AccountService.GetOpeningBalance();
            
            return Json(openbala, JsonRequestBehavior.AllowGet);

        }

    }
}