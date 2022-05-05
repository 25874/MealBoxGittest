using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MealBox.Models;

namespace MealBox.Services
{
    public class WareHouseService
    {
        MealBoxesEntities Db = new MealBoxesEntities();

        public void AddWareHouse(tbl_WareHouse Model) 
        {
            try
            {
                Db.tbl_WareHouse.Add(Model);
                Db.SaveChanges();
            }
            catch(Exception  ) 
            {
                
            }
        }

        public void AddWareHouseInventory(tbl_WareHouseInventory Model)
        {
            try
            {
                Db.tbl_WareHouseInventory.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception)
            {

            }
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
                         }).ToList();
            return query;
        }
    
    }
}