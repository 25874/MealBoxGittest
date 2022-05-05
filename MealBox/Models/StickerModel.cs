using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBox.Models
{
    public class StickerModel
    {
        public int StickerID { get; set; }
        public string BarCode { get; set; }
        public string Product { get; set; }
        public Nullable<double> Price { get; set; }
        public string Size { get; set; }
        public string Company { get; set; }
    }
}