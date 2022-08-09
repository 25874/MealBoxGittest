using MealBoxCloud;
using MealBoxCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBoxCloud.Services
{
    public class DashBoardService
    {
        MealBoxesEntities Db = new MealBoxesEntities();

        public object GetBufferStock()
        {
            var StockList = Db.stockIns.ToList();
            var ProdcutList = Db.Products.ToList();

            var query = (from a in ProdcutList
                         join b in StockList
                         on a.ProductID equals b.StockInID
                         where a.Limit >= b.StockQty
                         select new DSRModel
                         {
                             ProductID = a.ProductID,
                             ProductName = a.ProductName,
                             Limit = a.Limit,
                             Qty = b.StockQty 

                         }).ToList();

            return query;

        }
        public int GetCustomerNo() 
        {
            var data = Db.Customers_.Where(w => w.IsActive == true).Count();
            return data;
        }
        public int GetBookerNo() 
        {
            var data = Db.tbl_bkr.Where(w => w.IsActive == true).Count();
            return data;
        }
        public int GetSupplier()
        {
            var data = Db.suppliers.Where(w => w.IsActive == true).Count();
            return data;
        }

    }
}