using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBoxCloud.Models
{
    public class PosModel
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public ICollection<Sp_PosInvoice_Result>  sp_PosInvoice   { get; set; }

        public int EmployeeId { get; set; }

        public string InvoiceNum { get; set; }
        public string Accno { get; set; }

        public string ReturnType { get; set; }

        public string Name { get; set; }



        public Nullable<DateTime> BiilDate { get; set; }
        public string NtnNo { get; set; }

        public string per7amt { get; set; }

        public string per3amt { get; set; }

        public float Amount { get; set; }

        public string BillNo { get; set; }

        public string CustomerName { get; set; }

        public string CellNo { get; set; }
        public Nullable<bool> C17Per { get; set; }
        public Nullable<bool> C3Per { get; set; }
        public Nullable<bool> isCreadit { get; set; }
        public Nullable<bool> isCash { get; set; }
        public Nullable<bool> isDiscount { get; set; }

        public Nullable<Double> Balance { get; set; }
        public Nullable<double> DiscountAmount { get; set; }
        public Nullable<double> AmountPaid { get; set; }
        public Nullable<double> TotalAmount { get; set; }

        public List<PosChild> posChildren { get; set; }

        public List<Postable> postables { get; set; }
    }

    public class PosChild
    {
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ItemQty { get; set; }
        public Nullable<Double> ItemCost { get; set; }
        public Nullable<Double> ItemTotalAmount { get; set; }
        public Nullable<int> MSalChildID { get; set; }
        public int DSalId { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> Amount { get; set; }

    }

    public class Postable
    {
        public string BillNo { get; set; }
        public Nullable<DateTime> BiilDate { get; set; }
        public string CellNo { get; set; }
    }

}