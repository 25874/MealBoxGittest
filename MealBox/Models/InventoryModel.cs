using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MealBox.Models
{
    public class InventoryModel
    {        
       public string Product { get; set; }
       
        public int StockID    { get; set; } 
       public int  StockInID  { get; set; }
       public int  ProductTypeID  { get; set; }
       public Nullable<int>  StockQty  { get; set; }
       public string Units       { get; set; }
       public float weight      { get; set; }
      
        public DateTime Date        { get; set; }
       public Nullable<double>  unitprice   { get; set; }
       public float amount      { get; set; }
       public string created_by  { get; set; }
       //public DateTime create_at   { get; set; }
       //public DateTime Updated_at  { get; set; }
       public int StockRef    { get; set; }
       public string Type    { get; set; }
       
       public string Remarks { get; set; }
        public bool Defected { get; set; }

    }
}