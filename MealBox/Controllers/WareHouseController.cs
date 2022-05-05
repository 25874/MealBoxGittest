using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealBoxes.Infrastructure;
using AutoMapper;
using MealBox.Models;
using MealBox.Services;

namespace MealBox.Controllers
{
    public class WareHouseController : Controller
    {

        MealBoxesEntities Db = new MealBoxesEntities();
        private readonly WareHouseService _wareHouseService;
        private readonly IMapper _mapper;
        // GET: WareHouse

        public WareHouseController()
        {
            _wareHouseService = new WareHouseService();
            _mapper = AutoMapperProfile.Mapper;
        }
        public ActionResult Index()
        {
            using (var Db = new MealBoxesEntities()) 
            {
                var CityList =  Db.Cities.ToList();
                ViewBag.CityID = new SelectList(CityList, "CityId", "CityName");

                var WarHouseList = Db.tbl_WareHouse.ToList();
                ViewBag.WarHouseIdFk = new SelectList(WarHouseList, "WarHouseId", "WarHouseName");

                var ProductList = Db.Products.ToList();
                ViewBag.ProductId = new SelectList(ProductList, "ProductID", "ProductName");

                WareHouseModel Model = new WareHouseModel();

                var WareHouseList = _wareHouseService.GetWareHousList();

                var WareHouseData = _mapper.Map<List<WareHouse>>(WareHouseList);
                Model.WareHousesList = WareHouseData;

                return View(Model);
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(WareHouseModel Model)
        {

            var type = Model.Type;
            if (type == 1)
            {
                var data = Db.tbl_WareHouse.Count() + 1;
                Model.WareHouseCode = "00" + data.ToString();

                var Data = _mapper.Map<tbl_WareHouse>(Model);
                _wareHouseService.AddWareHouse(Data);

                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else 
            {
                var Data = _mapper.Map<tbl_WareHouseInventory>(Model);
                _wareHouseService.AddWareHouseInventory(Data);
                return Json("2", JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult WarHouseBody()
        {

            var WareHouseList = _wareHouseService.GetWareHousList();

            var WareHousedata = _mapper.Map<List<WareHouse>>(WareHouseList);

            return PartialView("_WareHouseBody", WareHousedata);
        }

    }
}