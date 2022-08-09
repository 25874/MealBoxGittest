using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MealBoxCloud.Models;
using System.Data.Entity;
using MealBoxCloud;

namespace MealBoxCloud.Services
{
    public class WareHouseService
    {
        MealBoxesEntities Db = new MealBoxesEntities();

        public string AddWareHouse(WareHouseModel WarehouseModel) 
        {

            var msg = "";
            var EditId = WarehouseModel.WarHouseIdFk;
            try
            {

                    if (EditId != 0)
                    {
                        var Warehouse = Db.tbl_WareHouse.Where(w => w.WarHouseId == EditId).FirstOrDefault();
                        Warehouse.CityID = WarehouseModel.CityID;
                        Warehouse.AreaLength = WarehouseModel.AreaLength;
                        Warehouse.WareHouseCode = Warehouse.WareHouseCode;
                        Warehouse.WarHouseName = WarehouseModel.WarHouseName;
                        Warehouse.CreatedBy = WarehouseModel.CreatedBy;
                        Warehouse.Description = WarehouseModel.Description;
                        Warehouse.CreateAt = DateTime.Now;
                        UpdateWareHouse(Warehouse);
                        msg = "1";
                    }
                    else 
                    {
                    var ProductExist = Db.tbl_WareHouse.Where(w => w.CityID == WarehouseModel.CityID && w.WarHouseName == WarehouseModel.WarHouseName).FirstOrDefault();
                    if (ProductExist == null)
                    {

                        var data = Db.tbl_WareHouse.Count() + 1;
                        WarehouseModel.WareHouseCode = "00" + data.ToString();

                        tbl_WareHouse obj = new tbl_WareHouse();
                        obj.CityID = WarehouseModel.CityID;
                        obj.AreaLength = WarehouseModel.AreaLength;
                        obj.WareHouseCode = WarehouseModel.WareHouseCode;
                        obj.CreatedBy = WarehouseModel.CreatedBy;
                        obj.WarHouseName = WarehouseModel.WarHouseName;
                        obj.Description = WarehouseModel.Description;
                        obj.CreateAt = DateTime.Now;
                        Db.tbl_WareHouse.Add(obj);
                        Db.SaveChanges();
                        msg = "1";

                    }
                    else
                    {
                        msg = "Already1";
                    }

                }
                
                
            }
            catch(Exception) 
            {
                
            }
            return msg;
        }

        public List<AreaLenght> GetAreaLenth()
        {
            return Db.AreaLenghts.ToList();
        }
        public string AddWareHouseInventory(WareHouseModel Model)
        {

            var msg = "Save";
            try
            {
              
                var Productid = Model.ProductId;
                var ProductExist = Db.tbl_WareHouseInventory.Where(w => w.WareHouseID == Model.WarHouseId && w.Productid == Productid).FirstOrDefault();
                if(ProductExist == null)
                {
                    tbl_WareHouseInventory obj = new tbl_WareHouseInventory();
                    obj.Productid = Productid;
                    obj.WareHouseID = Model.WarHouseId;
                    obj.CreatedBy = Model.CreatedBy;
                    obj.CreatedAt = DateTime.Now;
                    obj.Qty = 0;
                    Db.tbl_WareHouseInventory.Add(obj);
                    Db.SaveChanges();

                }
                else 
                {
                    msg = "Already2";
                }

            }
            catch(Exception)
            {

            }
            return msg;
        }
        public List<WareHouse> GetWareHousList() 
        {
         var warhouse = Db.tbl_WareHouse.ToList();
         var city = Db.Cities.ToList();

          var query = (from a in warhouse
                       join b in city
                       on a.CityID equals b.CityId
                       select new WareHouse
                       {
                           WarHouseName = a.WarHouseName,
                          AreaLength = a.AreaLength,
                           CityName = b.CityName,
                           WarHouseId = a.WarHouseId
                       }).OrderByDescending(o=> o.WarHouseId).ToList();
          return query;
        }


        public List<WareHouseInv> GetWareHousInveList()
        {
            var warhouseInv = Db.tbl_WareHouseInventory.ToList();
            var Product = Db.Products.ToList();
            var WareHouse = Db.tbl_WareHouse.ToList();

            var query = (from a in warhouseInv
                         join b in Product
                         on a.Productid equals b.ProductID
                         join c in WareHouse
                         on a.WareHouseID equals c.WarHouseId
                         select new WareHouseInv
                         {
                             ProductId = a.Productid.Value,
                             ProductName = b.ProductName,
                             WarHouseId = c.WarHouseId,
                             WarHouseName = c.WarHouseName,
                             Qty = a.Qty.Value

                         }).ToList();
           
            return query;
        }

        public tbl_WareHouse GetWarehouse(int id) 
        {
            var Data = Db.tbl_WareHouse.Where(w => w.WarHouseId == id).FirstOrDefault();
            return Data;
        }

        public void UpdateWareHouse(tbl_WareHouse model)
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
    }
}