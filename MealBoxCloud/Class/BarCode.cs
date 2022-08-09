
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MealBoxCloud.Class
{
    public class BarcodeM 
    {

        public string GenerateBarcode(string barcode) 
        { 
         using (MemoryStream memoryStream = new MemoryStream())    
      {    
        using (Bitmap bitMap = new Bitmap(barcode.Length* 40, 80))    
        {    
            using (Graphics graphics = Graphics.FromImage(bitMap))    
            {    
                Font oFont = new Font("IDAutomationHC39M", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);    
                SolidBrush blackBrush = new SolidBrush(Color.Black);
              graphics.DrawString("*" + barcode + "*", oFont, blackBrush, point);    
            }
              bitMap.Save(memoryStream, ImageFormat.Jpeg);
              var data = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
             return data;
        }    
     }  
   }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }


        //public Byte[] getBarcodeImage(string barcode, string file)
        //{
        //    try
        //    {
        //        Barcode39 _barcode = new Barcode39();
        //        int barSize = 16;
        //        string fontFile = HttpContext.Current.Server.MapPath("~/fonts/FREE3OF9.TTF");
        //        return (_barcode.CodeType(barcode, barSize, true, file, fontFile));

        //    }
        //    catch (Exception ex)
        //    {
        //        //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);  
        //    }
        //    return null;
        //}

    }
}
