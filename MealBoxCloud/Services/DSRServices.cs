using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MealBoxCloud.Models;
using MealBoxCloud;

namespace MealBox.Services
{
    public class DSRServices
    {
        MealBoxesEntities Db = new MealBoxesEntities();
        public void CheakDate(int id ,DateTime date) 
        {
            var Data = Db.DsrStocks.Where(w => w.ProductId == id && w.PurchaseDate == date).FirstOrDefault();
        }

        public DsrStock GetDsrStock(int id)
        {
            return Db.DsrStocks.Where(w => w.ProductId == id).FirstOrDefault();
        }
        public void UpdateDsrStock(DsrStock model) 
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }
        public List<DSRModel> GetDsrList() 
        {
            var BookList     =        Db.tbl_User.ToList();
            var MDsr         =        Db.tbl_Mdsr.ToList();
            var areaList     =        Db.tbl_area.ToList();
            var CustomerList =        Db.Customers_.ToList();
            var ProductList  =        Db.Products.ToList();
            var Ddsr         =        Db.tbl_ddsrbk.ToList();

            var query = (from a in MDsr
                         join b in BookList
                         on a.BookerId equals b.EmployeeId
                         join c in CustomerList
                         on a.CustomerID equals c.CustomerID
                         join y in areaList 
                         on a.areaid equals y.areaid
                         where(b.UserTypeId == 2)
                         //join e in ProductList
                         //on d.ProductID equals e.ProductID into left
                         //from leftdata in left.DefaultIfEmpty()
                         select new DSRModel
                         {
                             CustomerName = c.CustomerName,
                             BookerName = b.Name,
                             AreaName = y.area_,
                             ttlamt = a.TotalAmount,
                             Discount = a.Discount,
                             Purchasedate = a.dsrdat,
                             MdsrId = a.dsrid
                             //ProductName = leftdata == null ? String.Empty : leftdata.ProductName
                         }).ToList();

            return query;
        }

        public tbl_Mdsr GetMdsr(int id)
        {
            return Db.tbl_Mdsr.Find(id);
        }

        public List<Sp_DsrView_Result> DsrInvoice(int id)
        {
            var data = Db.Sp_DsrView(id).ToList();
            return data;
        }

        public List<SP_BookingSheet_Result> BookingSheet(int id , string date)
        {
            var data = Db.SP_BookingSheet(date, id).ToList();
            return data;
        }

        public List<VerifyBooking_Result> VerifyBookingSheet(int id, string date)
        {
            var data = Db.VerifyBooking(id,date).ToList();
            return data;
        }

        public void Updatemdsr(tbl_Mdsr model)
        {
            try
            {
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public List<ViewFinalDsr_Result> Finaldsr() 
        {
            var Data = Db.ViewFinalDsr().ToList();
            return Data;
        }
    }
}