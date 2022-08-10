using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MealBoxCloud.Models
{
    public class PurchaseModel
    {

        public ICollection<Sp_PurchaseInvoice_Result> PurchaseInvoice { get; set; }
        public int MPurID { get; set; }
        public string VndrNam { get; set; }
        public string VndrAdd { get; set; }
        public string VndrCntct { get; set; }
        public string PurNo { get; set; }
        public string Accno { get; set; }
        public string InvoiceNum { get; set; }
        public string CustomerName { get; set; }
        public string ReturnType { get; set; }

        public Nullable<int> wareHouseID { get; set; }
        public int ReturnTypeId { get; set; }
        public Nullable<double> Discount { get; set; }
        public int BankId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> MPurDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CNIC { get; set; }
        public string NTNNo { get; set; }
        public Nullable<bool> ToBePrntd { get; set; }
        public Nullable<bool> ck_Act { get; set; }
        public Nullable<bool> Effected { get; set; }
        public string MPurRmk { get; set; }
        public Nullable<int> ven_id { get; set; }
        public Nullable<int> PayTyp_id { get; set; }
        public Nullable<int> vchtyp_id { get; set; }
        public Nullable<double> csh_amt { get; set; }
        public Nullable<int> CashBnk_id { get; set; }
        public Nullable<int> chque_No { get; set; }
        public Nullable<int> subheadcategoryfourID { get; set; }
        public Nullable<double> Out_Standing { get; set; }
        public Nullable<double> grssttl { get; set; }

        public Nullable<double> PreviousBalnace { get; set; }

        public string ProductName { get; set; }

        public Nullable<int> ProductID { get; set; }

        public Nullable<double> Qty { get; set; }

        public Nullable<double> NetTotal { get; set; }

        public Nullable<double> hNetTotal { get; set; }

        public Nullable<double> Amount { get; set; }

        public Nullable<double> PurchaseTotal { get; set; }

        public Nullable<double> Cost { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public string DCNO { get; set; }
        public string DatTim { get; set; }
        public string BiltyNo { get; set; }
        public string VehicalNo { get; set; }
        public string DriverNam { get; set; }
        public string DriverMobilno { get; set; }
        public string Transporter { get; set; }
        public string station { get; set; }
        public string DilverOrdr { get; set; }
        public Nullable<double> frieght { get; set; }
        public Nullable<double> GrossTtal { get; set; }
        public string PayAcc { get; set; }

        public int DpurHid { get; set; }
        public int Itemhid { get; set; }
        public List<ItemChild> ItemChild { get; set; }

        public List<DeleteItemChild2> deleteItems { get; set; }
    }

    public class DeleteItemChild2
    {
        public int DpurId { get; set; }
        public int PrdctId { get; set; }
        public int Qty2 { get; set; }

    }

    public class ItemChild
    {
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> ItemQty { get; set; }
        public Nullable<Double> ItemCost { get; set; }
        public Nullable<Double> ItemTotalAmount { get; set; }
        public Nullable<int> MPurChildID { get; set; }
        public int DpurId { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> Amount { get; set; }

    }


}