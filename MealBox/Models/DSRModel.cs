using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBox.Models
{
    public class DSRModel
    {
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> Salesman { get; set;}
        public Nullable<int> areaid { get; set;}
        public Nullable<double> furout { get; set;}
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> untid { get; set;}
        public Nullable<double> Qty { get; set; }
        public Nullable<double> salrat { get; set; }
        public Nullable<double> salrturn { get; set; }
        public Nullable<double> recvry { get; set; }
        public Nullable<double> outstan { get; set; }
        public Nullable<double> Amt { get; set; }
        public Nullable<double> saleper { get; set; }
        public string CustomerName { get; set; }

        public int BookeriD { get; set; }
        public string AreaName { get; set; }

        public string BookerName { get; set; }

        public string ProductName { get; set; }
        public Nullable<double> TotalAfterDiscount { get; set; }
        public Nullable<int> Discount { get; set; }
        public string dsrrmk { get; set; }
        public Nullable<System.DateTime> DsrDate { get; set; }
        public Nullable<double> ttlamt { get; set; }
        public int Paytype { get; set; }
        public string Comment { get; set; }

        public Nullable<int> Limit { get; set; }
        public Nullable<System.DateTime> Purchasedate { get; set; }

        public Nullable<int> RemainingQuantity { get; set; }

        public int MdsrId { get; set; }
        public List<DsrChild> dsrChild { get; set; }
    }

    public class DsrChild 
    {
     public Nullable<int>    ChildProductID { get; set; }
     public Nullable<int>      Childuntid    { get; set; }
     public Nullable<int>      ChildQty   { get; set; }
     public Nullable<double>   Childsalrat   { get; set; }
     public Nullable<double>   Childsalrturn { get; set; }    
     public Nullable<double>   ChildAmt      { get; set; }
     public Nullable<double>   saleper       { get; set; }
     public int Ddsrid { get; set; }

     public string ChildProductName { get; set; }

    }
}