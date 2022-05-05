using MealBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MealBox.Services
{
    public class InventoryService
    {
        MealBoxesEntities Db = new MealBoxesEntities();

        public List<Product>  GetProductList() 
        {
            return Db.Products.ToList();
        }

        public void AddStockIn(stockIn Model)
        {
            try
            {
                Db.stockIns.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        public int StockCount() 
        {
            var data =  Db.stockIns.Count();
            return data;
        }

        public List<InventoryModel> StockList() 
        {
            var StockList = Db.stockIns.ToList();
            var ProductList = Db.Products.ToList();

            var query = (from a in ProductList
                         join b in StockList
                         on a.ProductID equals b.StockInID
            
                         select new InventoryModel
                         {
                             Product = a.ProductName,
                             unitprice = b.unitprice,
                             StockQty = b.StockQty,
                             StockID = b.StockID
                         }).ToList();
            
            return query;
        }

        public stockIn GetStock(int id)
        {
            return Db.stockIns.Where(w => w.StockInID == id).FirstOrDefault();
        }

        public void UpdateStock(stockIn model)
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

        public object GetQuantityAvail(int id) 
        {
           
              var  quantavail = Db.stockIns.Where(w => w.StockInID == id).Select(s => new
                {
                    PurchaseDate = s.Date.ToString(),
                    RemainingQuantity = s.StockQty
                }).FirstOrDefault();
            return quantavail;
        }

          
    
    }
}