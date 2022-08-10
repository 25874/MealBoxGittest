using AutoMapper;
using MealBoxCloud.Infrastructure;
using MealBoxCloud.Models;
using MealBoxCloud.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MealBoxCloud.Controllers
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
        public ActionResult Index(int? id)
        {

            var area = _wareHouseService.GetAreaLenth();
            ViewBag.AreaLenght = new SelectList(area, "AreaLenghtid", "AreaLenght1");


            if (id > 0)
            {

                WareHouseModel Model = new WareHouseModel();

                using (var db = new MealBoxesEntities())
                {
                    var MsalId = _wareHouseService.GetWarehouse(id.Value);
                    Model.AreaLength = MsalId.AreaLength;
                    Model.WarHouseName = MsalId.WarHouseName;
                    Model.Description = MsalId.Description;
                    Model.CityID = MsalId.CityID;
                    Model.EditId = MsalId.WarHouseId;
                    var json = new JavaScriptSerializer().Serialize(Model);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
            }


            using (var Db = new MealBoxesEntities())
            {
                var CityList = Db.Cities.ToList();
                ViewBag.CityID = new SelectList(CityList, "CityId", "CityName");

                var WarHouseList = Db.tbl_WareHouse.ToList();
                ViewBag.WarHouseIdFk = new SelectList(WarHouseList, "WarHouseId", "WarHouseName");

                var ProductList = Db.Products.ToList();
                ViewBag.ProductId = new SelectList(ProductList, "ProductID", "ProductName");

                WareHouseModel Model = new WareHouseModel();

                var WareHouseList = _wareHouseService.GetWareHousList();


                var WareHouseData = _mapper.Map<List<WareHouse>>(WareHouseList);

                Model.WareHousesList = WareHouseData;

                var WareHouseInvList = _wareHouseService.GetWareHousInveList();


                var WareHouseInvdata = _mapper.Map<List<WareHouseInv>>(WareHouseInvList);

                Model.WareHousesInvList = WareHouseInvdata;

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

                var data2 = _wareHouseService.AddWareHouse(Model);

                return Json(data2, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var data = _wareHouseService.AddWareHouseInventory(Model);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult WarHouseBody()
        {

            var WareHouseList = _wareHouseService.GetWareHousList();

            var WareHousedata = _mapper.Map<List<WareHouse>>(WareHouseList);

            return PartialView("_WareHouseBody", WareHousedata);
        }

        public ActionResult WarHouseInventoryBody()
        {

            var WareHouseInvList = _wareHouseService.GetWareHousInveList();

            var WareHouseInvdata = _mapper.Map<List<WareHouseInv>>(WareHouseInvList);

            return PartialView("WareHouseInvbody", WareHouseInvdata);
        }
    }
}