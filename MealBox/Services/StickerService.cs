using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBox.Services
{
    public class StickerService
    {

        MealBoxesEntities Db = new MealBoxesEntities();


        public List<Sticker> GetStickerlist()
        {
            return Db.Stickers.ToList();
        }


    }
}