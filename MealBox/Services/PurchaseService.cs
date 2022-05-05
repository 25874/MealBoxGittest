using MealBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MealBox.Services
{
    public class PurchaseService 
    {
        MealBoxesEntities Db = new MealBoxesEntities();
        public List<PurchaseModel> PurchaseList()
        {
            var Purchase = Db.MPurchases.ToList();
            var Supplier = Db.suppliers.ToList();
            var PurchaseD = Db.DPurchases.ToList();
              

            var query = (from a in Purchase
                         join b in Supplier
                         on a.ven_id equals b.supplierId
                         join c in PurchaseD
                         on a.MPurID equals c.MPurID
                         select new PurchaseModel 
                         {
                             VndrNam  = b.suppliername,
                             MPurDate = a.MPurDate,
                             MPurID   = a.MPurID,
                             PurNo    = a.PurNo,
                             Qty      = c.Qty,
                             ProductName = c.Product1.ProductName.ToString(),
                             Effected = a.Effected.GetValueOrDefault()                             
                         }).ToList();            
            
            return query;
        }       
        public MPurchase GetPurchase(int Id)
        {
            return Db.MPurchases.Find(Id);
        }

        public stockIn GetStock(int id ) 
        {
            return Db.stockIns.Where(w => w.StockInID == id).FirstOrDefault();
        }

        public void UpdateStock(stockIn model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void UpdatePurchaseCreadit(tbl_Purcredit model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }


        public object GetAccNo(int id)
        {
            var Data = Db.Accounts.Where(w => w.PersonId == id).Select(s => s.AccountGeneratedCodeId).FirstOrDefault();
            return Data;
        }
        public tbl_Purcredit SupllierExist(string id) 
        {
            var SupplierId = Db.tbl_Purcredit.Where(w => w.supplierId == id).FirstOrDefault();
            return SupplierId;
                
        }
        public string GetAccountCode(int? id)
        {
            var Query = Db.Accounts.Where(w => w.PersonId == id).Select(s => s.AccountGeneratedCodeId).FirstOrDefault();
            return Query;
        }

        public object GetCreadit(string id)
        {

            var Query = Db.tbl_Purcredit.Where(w => w.supplierId == id).Select(s => s.CredAmt).FirstOrDefault();

            return Query;
        }

        public List<Sp_PurchaseInvoice_Result>  PurchaseInvoice(int id) 
        {
            var data = Db.Sp_PurchaseInvoice(id).ToList();
            return data;
        }

        public double GetOpeningBalance()
        {

            var amovar = Db.tbl_CB.OrderByDescending(o => o.CB_id).Select(s => s.AmountVar).FirstOrDefault();

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

        public int? GetpersonId(string id)
        {
            var Data = Db.Accounts.Where(w => w.AccountGeneratedCodeId == id).Select(s => s.PersonId).FirstOrDefault();
            return Data;
        }

    }
}