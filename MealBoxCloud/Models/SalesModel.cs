using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBoxCloud.Models
{
    public class SalesModel
    {
        public int MSaleid {get; set;}
        public string MSal_sono { get; set; }
        public Nullable<System.DateTime> MSal_dat { get; set; }
        public string MSal_Rmk { get; set; }
        public Nullable<int> MSalOrdid { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string Accno { get; set; }
        public string InvoiceNum { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> ApplyDis { get; set; }
        public Nullable<int> wareHouseID { get; set; }
        public string ReturnType { get; set; }
        public float Prevebalanace { get; set; }
        public double applydiscountamt { get; set; }
        public int ReturnTypeId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> ISActive { get; set; }
        public Nullable<bool> iscre { get; set; }
        public Nullable<bool> iscash { get; set; }
        public string gatpassno { get; set; }
        public string schm { get; set; }
        public string bons { get; set; }
        public Nullable<double> Recovery { get; set; }
        public Nullable<double> Outstanding { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public string username { get; set; }
        public string Booker { get; set; }
        public string SalMan { get; set; }
        public Nullable<int> promethod { get; set; }
        public Nullable<int> MProQuot_id { get; set; }
        public string custacc { get; set; }
        //pos
        public Nullable<double> Balance { get; set; }

        public ICollection<Sp_SaleInvoice_Result> SaleInvoice { get; set; }
        public string  ProductName { get; set; }
        public Nullable<bool> Effected { get; set; }
        public Nullable<double> gst { get; set; }
        public Nullable<double> othtax { get; set; }
        public Nullable<double> Amt { get; set; }
        public Nullable<double> hAmt { get; set; }
        
        public Nullable<double> GTtl { get; set; }
        public Nullable<double> Dis { get; set; }
        public Nullable<double> sdis { get; set; }
        public Nullable<double> sdisamt { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> PreBalance { get; set; }
        public Nullable<double> DSal_salcst { get; set; }
        public Nullable<double> DSal_ItmQty { get; set; }
        public Nullable<int> ProductID { get; set; }
        public int DSalHid { get; set;}
        public List<SalesChild> saleschilds {get; set;}
        public List<DeleteSalesChild> SaleDele { get; set; }
        public Nullable<double> DSal_netttl { get; set; }

        //public List<Postable> Postable { get; set; }
    }
    public class SalesChild
    {
        public Nullable<int> ItemId { get; set; }
        public Nullable<int>    ItemQty { get; set; }
        public Nullable<Double> ItemCost { get; set; }
        public Nullable<Double> ItemTotalAmount { get; set; }
        public Nullable<int> MSalChildID { get; set; }
        public int DSalId { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> Amount { get; set; }

    }


    public class DeleteSalesChild
    {
        public int DSaleId { get; set; }

        public int ItemDelId { get; set; }
        public int ItemDelQty { get; set; }
        public Nullable<Double> ItemCost { get; set; }
        public int SaleReturnId { get; set; }
    
        public Nullable<Double> ItemTotalAmount { get; set; }
    }

}