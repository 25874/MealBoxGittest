﻿using AutoMapper;
using MealBoxCloud.Infrastructure;
using MealBoxCloud.Models;
using MealBoxCloud.Services;
using System;
using System.Web.Mvc;

namespace MealBoxCloud.Controllers
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
            var UpdateProduct = _inventoryService.GetStock(ProductId);
            UpdateProduct.StockQty = UpdateProduct.StockQty - Model.StockQty;
            UpdateProduct.unitprice = Model.unitprice;
            UpdateProduct.amount = Model.amount;
            UpdateProduct.create_at = DateTime.Now;
            _inventoryService.UpdateStock(UpdateProduct);
            return Json("succes", JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeductInventory()
        {
            var ProductList = _ProductService.ProductTypeList();
            ViewBag.ProductTypeID = new SelectList(ProductList, "ProductTypeID", "ProductTypeName");
            ViewBag.Product = new SelectList("");
            return View();
        }


        [HttpPost]
        public ActionResult DeductInventory(InventoryModel Model)
        {
            var ProductId = Model.StockInID;

            var UpdateProduct = _inventoryService.GetStock(ProductId);
            UpdateProduct.StockQty = UpdateProduct.StockQty - Model.StockQty;
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