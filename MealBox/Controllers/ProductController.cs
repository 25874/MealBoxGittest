using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBoxes.Services;
using MealBoxes.Infrastructure;
using AutoMapper;
using MealBox.Services;
using MealBox.Models;

namespace MealBox.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        private readonly ProductServices _productServices;
        
        private readonly IMapper  _mapper;
        public ProductController()
        {
            _productServices = new ProductServices();
            _mapper = AutoMapperProfile.Mapper;
        }
        
        public ActionResult AddItem(int ? id)
        {
            using(var db = new MealBoxesEntities())
            {
                var ProductTypeList = db.tbl_producttype.ToList();
                ViewBag.ProductTypeID = new SelectList(ProductTypeList, "ProductTypeID", "ProductTypeName");

                if(id > 0)
                {
                    var data = _productServices.GetProduct(id.Value);
                    var ModalData = _mapper.Map<ProductModel>(data);
                    return View(ModalData);
                }
                
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult AddItem(ProductModel Model)
        {
            using(var db = new MealBoxesEntities())
            {
                try
                {
                    var ProductList = db.tbl_producttype.ToList();
                    ViewBag.ProductTypeID = new SelectList(ProductList, "ProductTypeID", "ProductTypeName");
                    var id = Model.ProductID;
                    if (id == 0)
                    {
                        Product obj = new Product();
                        obj.ProductName = Model.ProductName;
                        obj.ProductTypeID = Model.ProductTypeID;
                        obj.PurchasePrice = Model.PurchasePrice;
                        obj.SalePrice = Model.SalePrice.GetValueOrDefault();
                        obj.stk_carton = Model.stk_carton;
                        obj.PckSize = Model.PckSize;
                        obj.Unit = Model.Unit;
                        obj.CreatedAt = Model.CreatedAt.GetValueOrDefault();
                        obj.Limit = Model.Limit;
                        db.Products.Add(obj);
                        db.SaveChanges();
                        var olditem = _productServices.GetStockId(obj.ProductID);
                        var stockref = db.stockIns.Count();
                        stockIn obj2 = new stockIn();
                        obj2.StockInID  = olditem.ProductID;
                        obj2.StockRef   = stockref;
                        obj2.StockQty   = 0;
                        obj2.create_at =  DateTime.Now;
                        obj2.created_by = "Sajid";
                        obj2.Units = Model.Unit;
                        obj2.StockQty = 0;
                        db.stockIns.Add(obj2);
                        db.SaveChanges();
                        DsrStock obj3 = new DsrStock();
                        obj3.DsrQuantity = 0;
                        obj3.ProductId = olditem.ProductID;
                        db.DsrStocks.Add(obj3);
                        db.SaveChanges();
                        return Json("Success", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        var Product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                        Product.ProductName = Model.ProductName;
                        Product.ProductTypeID = Model.ProductTypeID;
                        Product.PurchasePrice = Model.PurchasePrice;
                        Product.SalePrice = Model.SalePrice.GetValueOrDefault();
                        Product.stk_carton = Model.stk_carton;
                        Product.PckSize = Model.PckSize;
                        Product.Unit = Model.Unit;
                        Product.Limit = Model.Limit;
                        db.SaveChanges();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                  
                }
                  
                catch (Exception ex) 
                {

                }
                return View();
            }

        }
        public ActionResult ItemList()
        {
                var Model = _productServices.ProductList();
                
                return View(Model);
        }
        public ActionResult AddItemCategory(int? id) 
        {
            if(id > 0)
            {
                var data = _productServices.GetProductType(id.Value);
                var ModalData = _mapper.Map<ProductModel>(data);
                return View(ModalData);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddItemCategory(ProductModel Model)
        {
            var id = Model.ProductTypeID;
            if (id == 0 || id == null)
            {
                var Modeldata = _mapper.Map<tbl_producttype>(Model);
                _productServices.AddProductType(Modeldata);
            }
            else
            {
                var modeldata = _mapper.Map<tbl_producttype>(Model);
                _productServices.UpdateProductType(modeldata);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ItemTypeList() 
        {
            var List = _productServices.ProductTypeList();
            var Model = _mapper.Map<List<ProductModel>>(List);
            return View(Model);
        }

        public ActionResult Itemprice(int? id) 
        {
            var data = _productServices.GetProductBYId(id.Value);
            
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        
        //Get Product for Inventory
        public ActionResult GetProductType(int id)
        {
                var data = _productServices.GetProductType(id);
                return Json (data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductList(int id)
        {
            var data = _productServices.ProductListBYId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}