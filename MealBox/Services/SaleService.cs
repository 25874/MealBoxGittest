using MealBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;




namespace MealBox.Services
{
    public class SaleService
    {
        MealBoxesEntities Db = new MealBoxesEntities();
        public List<SalesModel> SaleList()
        {
            var Sales = Db.tbl_MSal.ToList();
            var Customer = Db.Customers_.ToList();
            var Dsale = Db.tbl_DSal.ToList();
            var Prdt = Db.Products.ToList();

            var query = (from a in Sales
                         join b in Customer
                         on a.CustomerID equals b.CustomerID
                         join c in Dsale
                         on a.MSal_id equals c.MSal_id
                         join d in Prdt
                         on c.ProductID equals d.ProductID
                         select new SalesModel
                         {
                             MSaleid = a.MSal_id,
                             MSal_dat = a.MSal_dat,
                             CustomerName = b.CustomerName,
                             CustomerID = b.CustomerID,
                             MSal_sono = a.MSal_sono                        
                         }).ToList();

            return query;
        }
        public tbl_MSal GetSale(int Id)
        {
            return Db.tbl_MSal.Find(Id);
        }
        public void UpdateSalesCreadit(SaleCreadit model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void UpdateSales(tbl_MSal model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }


        public SaleCreadit CustomerExist(int id)
        {
            var SupplierId = Db.SaleCreadits.Where(w => w.CustomerId == id).FirstOrDefault();
            return SupplierId;

        }

        public List<Sp_SaleInvoice_Result> GetSaleInvoice(int id) 
        {
            var data = Db.Sp_SaleInvoice(id).ToList();
            return data;
        }
        public object SAleCreadit(int id)
        {

            var Query = Db.SaleCreadits.Where(w => w.CustomerId == id).Select(s => s.CreaditAmount).FirstOrDefault();

            return Query;
        }

        public List<Mpos> GetPos()
        {
            var data = Db.Mpos.ToList();
            return data;
        }

        public double GetOpeningBalance()
        {

            var amovar = Db.tbl_CB.OrderByDescending(o => o.CB_id).Select(s => s.AmountVar).FirstOrDefault();

            if(amovar != null)
            {
                return amovar.Value;
            }
            else
            {
                amovar = 0;
            }

            return amovar.Value;

        }

        public object GetAccNo(int id) 
        {
            var Data = Db.Accounts.Where(w => w.PersonId == id).Select(s => s.AccountGeneratedCodeId).FirstOrDefault();
            return Data;
        }

        public int? GetpersonId(string id)
        {
            var Data = Db.Accounts.Where(w => w.AccountGeneratedCodeId == id).Select(s => s.PersonId).FirstOrDefault();
            return Data;
        }
        public object GetWalkin()
        {
            var Walkin = Db.Accounts.Where(w => w.PersonId == 3003).Select(s => new
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

        //public void AddPurchases(PurchaseModel Model)
        //{
        //    try
        //    {
        //        Db.MPurchases.Add(Model);
        //        Db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        Console.ReadLine();
        //    }
        //}

        //public void UpdatePurchase(MPurchase model)
        //{
        //    Db.Entry(model).State = EntityState.Modified;
        //    Db.SaveChanges();
        //}
    }
}