using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MealBox.Models;
using MealBox.Services;
using MealBoxes.Infrastructure;
using MealBoxes.Services;

namespace MealBox.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory

        private readonly IMapper _mapper;
        private readonly ProductServices _ProductService;
        private readonly InventoryService _inventoryService;
        public InventoryController()
        {
            _ProductService = new ProductServices();
            _mapper = AutoMapperProfile.Mapper;
            _inventoryService = new InventoryService();
        }
        public ActionResult AddInventory()
        {
            ViewBag.StockCount = _inventoryService.StockCount();
            var ProductList = _ProductService.ProductTypeList();
            ViewBag.ProductTypeID = new SelectList(ProductList, "ProductTypeID", "ProductTypeName");
            ViewBag.Product = new SelectList("");            
            return View();
        }

        [HttpPost]
        public ActionResult AddInventory(InventoryModel Model)
        {
            var ProductId = Model.StockInID;
            var ProductList = _ProductService.ProductTypeList();
            ViewBag.ProductTypeID = new SelectList(ProductList, "ProductTypeID", "ProductTypeName");
            var UpdateProduct = _inventoryService.GetStock(ProductId);
            UpdateProduct.StockQty = UpdateProduct.StockQty + Model.StockQty;
            UpdateProduct.unitprice = Model.unitprice;
            UpdateProduct.amount = Model.amount;
            UpdateProduct.create_at = DateTime.Now;
            _inventoryService.UpdateStock(UpdateProduct);
            return Json("succes", JsonRequestBehavior.AllowGet);
        }
        public ActionResult InventoryList() 
        {
            var model = _inventoryService.StockList();            
            return View(model);
        }

        public ActionResult StockById(int id) 
        {
            var Data = _inventoryService.GetStock(id);
            return Json(Data.StockQty, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuantityAvail(int id) 
        {
            var Data = _inventoryService.GetQuantityAvail(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        } 

    }
}