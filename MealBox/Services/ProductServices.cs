using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MealBox.Models;

namespace MealBox.Services
{
    public class ProductServices
    {
        MealBoxesEntities Db = new MealBoxesEntities();
        public List<ProductModel> ProductList()
        {
            var product = Db.Products.ToList();
            var _Producttypes = Db.tbl_producttype.ToList();

            var query = (from a in product
                         join b in _Producttypes
                        on a.ProductTypeID equals b.ProductTypeID
                         select new ProductModel
                         {
                             ProductTypeID = b.ProductTypeID,
                             ProductTypeName = b.ProductTypeName,
                             ProductID = a.ProductID,
                             ProductName = a.ProductName
                         }).ToList();

            return query;
        }

        public object ProductListBYId(int Id)
        {
            var query = Db.Products.Where(w => w.ProductTypeID == Id).Select(s => new
            {
                ProductID = s.ProductID,
                ProductName = s.ProductName

            }).ToList();

            return query;

        }

        public object GetProductBYId(int Id)
        {
            var ProductList = Db.Products.ToList();

            var StockList = Db.stockIns.ToList();



            var query = (from a in ProductList
                        join b in StockList
                        on a.ProductID equals b.StockInID
                         where (b.StockInID == Id)
                         select new 
                        { 

                PurchasePrice = a.PurchasePrice,
                Limit = a.Limit,
                SalePrice = a.SalePrice,
                UnitId = a.Unit,
                ProductName = a.ProductName,
                Quantity = b.StockQty
            
               }).FirstOrDefault();
                

            return query;
        }

       

        public Product GetProduct(int id) 
        {
            var data = Db.Products.Where(w => w.ProductID == id).FirstOrDefault();
            return data;
        }

        public void AddProduct(Product Model)
        {
            try
            {
                Db.Products.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        public void UpdateProduct(Product model) 
        {

            try
            {

            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        public List<tbl_producttype> ProductTypeList()
        {
            return Db.tbl_producttype.ToList();
        }

        public object  GetProductType(int Id)
        {
            var product = Db.Products.ToList();
            var _Producttypes = Db.tbl_producttype.ToList();

            var query = (from a in product
                         join b in _Producttypes
                        on a.ProductTypeID equals b.ProductTypeID
                         where (b.ProductTypeID == Id)
                         select new
                         {
                             ProducttypeId = b.ProductTypeID,
                             ProductTypeName = b.ProductTypeName,
                             ProductId = a.ProductID
                         }).ToList();

            return query;
        }

        

        public Product GetStockId(int id) 
        {
            return Db.Products.Find(id);
        }

        public void AddProductType(tbl_producttype Model)
        {
            try
            {
                Db.tbl_producttype.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
        public void UpdateProductType(tbl_producttype model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }

        
    }
}