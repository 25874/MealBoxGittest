using System.Collections.Generic;
using System.Linq;

namespace MealBoxCloud.Services
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