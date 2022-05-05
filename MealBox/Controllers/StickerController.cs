using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MealBox.Models;
using MealBox.Services;
using MealBoxes;
using MealBoxes.Infrastructure;

using MealBoxes.Services;

namespace MealBox.Controllers
{
    public class StickerController : Controller
    {
        // GET: Sticker

        MealBoxesEntities Db = new MealBoxesEntities();
        private readonly StickerService _StickerService;
        private readonly IMapper _mapper;

        BarCode _BarCode = new BarCode();
        public ActionResult Index()
        {
            return View();
        }

        public StickerController()
        {
            _BarCode = new BarCode();
            _StickerService = new StickerService();
            _mapper = AutoMapperProfile.Mapper;
        }

        [HttpPost]
        public ActionResult Index(StickerModel Model)
        {
            using (var db = new MealBoxesEntities()) 
            {
                Sticker obj = new Sticker();
                try
                {
                    obj.BarCode = _BarCode.GenerateBarcode(Model.BarCode);
                    obj.Company = Model.Company;
                    obj.Product = Model.Product;
                    obj.Price = Model.Price;
                    obj.Size = Model.Size;
                    db.Stickers.Add(obj);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {

                }
            }
            return View();
        }

        public ActionResult StickerList() 
        {
            var Datalist = _StickerService.GetStickerlist();
            var Model = _mapper.Map<List<StickerModel>>(Datalist);  
            return View(Model);
        }
    }
}