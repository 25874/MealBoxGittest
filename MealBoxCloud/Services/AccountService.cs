using MealBoxCloud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MealBoxCloud.Services
{
    public class AccountService : PurchaseService
    {
        MealBoxesEntities db = new MealBoxesEntities();
        public List<SubHeadCategoriesModel> Accounttbl()
        {
            var headlist = db.AccountHeads.ToList();
            var SubHead = db.SubHeads.ToList();
            var Account = db.Accounts.ToList();

            var Query = (from a in headlist
                         join b in SubHead
                         on a.HeadGeneratedIdCode equals b.HeadGeneratedIdCode
                         join c in Account
                         on a.HeadGeneratedIdCode equals c.headGeneratedIdCode
                         where (c.AccountName != null)
                         select new SubHeadCategoriesModel
                         {
                             HeadName = a.HeadName,
                             SubHeadName = b.SubHeadName,
                             AccountName = c.AccountName,
                             AccountGeneratedCodeeID = c.AccountGeneratedCodeId,

                         }).ToList();

            return Query;
        }
        public List<SubHeadModel> SubHeadtbl()
        {
            var headlist = db.AccountHeads.ToList();

            var SubHead = db.SubHeads.ToList();

            var Query = (from a in headlist
                         join b in SubHead
                         on a.HeadGeneratedIdCode equals b.HeadGeneratedIdCode

                         select new SubHeadModel
                         {
                             SubHeadGeneratedIdCode = b.SubHeadGeneratedIDCode,
                             SubHeadName = b.SubHeadName,
                             HeadGeneratedIdCodeID = a.HeadGeneratedIdCode,
                             HeadName = a.HeadName
                         }).ToList();

            return Query;
        }

        public object PayAccount()
        {
            var PayList = db.Accounts.Where(w => w.headGeneratedIdCode == "003" && w.AccountName != null).Select(s => new
            {

                AccNo = s.AccountGeneratedCodeId,
                AccName = s.AccountName

            }).ToList();
            return PayList;
        }

        public object RecvAccount()
        {
            var RecvList = db.Accounts.Where(w => w.headGeneratedIdCode == "001" && w.AccountName != null).Select(s => new
            {

                AccNo = s.AccountGeneratedCodeId,
                AccName = s.AccountName

            }).ToList();
            return RecvList;
        }


        public List<AccountModel> SubHeadDDl(string id)
        {
            var Headdata = db.AccountHeads.ToList();
            var Subhead = db.SubHeads.ToList();

            var query = (from a in Headdata
                         join b in Subhead
                         on a.HeadGeneratedIdCode equals b.HeadGeneratedIdCode
                         where (b.HeadGeneratedIdCode == id)
                         select new AccountModel
                         {
                             SubAccountId = b.SubHeadGeneratedIDCode,
                             SubAccountName = b.SubHeadName
                         }).ToList();
            return query;
        }


        //public List<SubHeadCategoriesModel> SubCategoryddl(int id)
        //{
        //var headlist = db.Heads.ToList();
        //var SubHead = db.SubHeads.ToList();
        //var SubheadCategory = db.SubHeadCategories.ToList();

        //var Query = (from a in headlist
        //join b in SubHead
        //on a.HeadGeneratedID equals b.HeadGeneratedID
        //join c in SubheadCategory
        //on a.HeadGeneratedID equals c.HeadGeneratedID
        //where (c.SubHeadCategoriesID == id)
        //select new SubHeadCategoriesModel
        //{
        //SubHeadGeneratedID = b.SubHeadGeneratedID,
        //SubHeadName = b.SubHeadName,
        //HeadGeneratedID = a.HeadGeneratedID,
        //HeadName = a.HeadName,
        //SubHeadCategoriesName = c.SubHeadCategoriesName,
        //SubHeadCategorieId = c.SubHeadCategoriesID
        //}).ToList();

        //return Query;
        //}


        public List<AccountHead> GetHeadsList()
        {
            return db.AccountHeads.ToList();
        }


        public void AddSubHead(AccountModel Model)
        {

            var HeadGenerateCode = Model.AccounId;

            var SubHeadcount = db.SubHeads.Count() + 1;

            var Acoountcount = db.Accounts.Count() + 1;

            var SubHeadCode = "002";

            var AccountCode = "003";


            SubHeadCode = SubHeadCode + SubHeadcount;

            AccountCode = AccountCode + Acoountcount;



            SubHead obj = new SubHead();
            obj.HeadGeneratedIdCode = HeadGenerateCode.ToString();
            obj.SubHeadName = Model.SubHeadName;
            obj.SubHeadGeneratedIDCode = SubHeadCode.ToString();
            db.SubHeads.Add(obj);


            tbl_Prefernce obj3 = new tbl_Prefernce();
            obj3.Headkey = SubHeadCode.ToString();
            obj3.headValue = Model.SubHeadName;
            db.tbl_Prefernce.Add(obj3);
            db.SaveChanges();


            Account obj2 = new Account();
            obj2.headGeneratedIdCode = HeadGenerateCode;
            obj2.SubheadGeneratedIdCode = SubHeadCode.ToString();
            obj2.AccountGeneratedCodeId = AccountCode.ToString();
            obj2.CreateBy = 2;
            obj2.CreatedAt = DateTime.Now;
            db.Accounts.Add(obj2);
            db.SaveChanges();

        }

        public void AddAccount(AccountModel model)
        {
            var Account = "";

            var AccountCode = "003";
            var Acoountcount = db.Accounts.Count() + 1;
            Account = AccountCode + Acoountcount;


            Account obj = new Account();
            obj.AccountGeneratedCodeId = Account;
            obj.headGeneratedIdCode = model.AccounId;
            obj.SubheadGeneratedIdCode = model.SubAccountId;
            obj.AccountName = model.SubAccountName;
            obj.CreateBy = 8;
            obj.CreatedAt = DateTime.Now;
            db.Accounts.Add(obj);
            db.SaveChanges();


        }

        public SaleCreadit CustomerExist(int id)
        {
            var SupplierId = db.SaleCreadits.Where(w => w.CustomerId == id).FirstOrDefault();
            return SupplierId;

        }

        public Head GetHeads(int id)
        {
            return db.Heads.Find(id);
        }

        public void UpdateSalesCreadit(SaleCreadit model)
        {
            db.Entry(model).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void UpdateHead(Head Model)
        {
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();

        }

        public double GetOpeningBalance()
        {

            var amovar = db.tbl_CB.OrderByDescending(o => o.CB_id).Select(s => s.AmountVar).FirstOrDefault();

            if (amovar != null)
            {
                return amovar.Value;
            }
            else
            {
                amovar = 0;
            }

            return amovar.Value;

        }

        public object GetWalkin()
        {
            var Walkin = db.Accounts.Where(w => w.PersonId == 3003).Select(s => new
            {
                HeadCode = s.headGeneratedIdCode,
                SubheadCode = s.headGeneratedIdCode,
                AccountCode = s.AccountGeneratedCodeId,
                Personid = s.PersonId,
                AccountName = s.AccountName
            })
            .FirstOrDefault();

            return Walkin;
        }

        public void AddHead(Head Model)
        {
            try
            {
                db.Heads.Add(Model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}