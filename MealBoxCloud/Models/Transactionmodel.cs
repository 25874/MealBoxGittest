using System;

namespace MealBoxCloud.Models
{
    public class Transactionmodel
    {
        public string Expenseremarks { get; set; }
        public int EmployeeId { get; set; }
        public string PayAccount { get; set; }
        public string RecAccount { get; set; }
        public string type { get; set; }
        public int expenceid { get; set; }
        public string acctitle { get; set; }
        public string accno { get; set; }
        public Nullable<System.DateTime> expensesdat { get; set; }
        public string billno { get; set; }
        public string typeofpay { get; set; }
        public string expencermk { get; set; }
        public Nullable<double> cashamt { get; set; }
        public Nullable<int> CashBnk_id { get; set; }
        public string CashBnk_nam { get; set; }
        public string BankId { get; set; }
        public int SaleReturn { get; set; }
        public int PurchaseReturn { get; set; }
        public Nullable<double> bankamt { get; set; }
        public Nullable<double> PaymentIn { get; set; }
        public Nullable<double> PaymentOut { get; set; }
        public Nullable<System.DateTime> ChqDat { get; set; }
        public Nullable<double> Amountpaid { get; set; }
        public Nullable<double> prevbal { get; set; }
        public Nullable<System.DateTime> createat { get; set; }
        public string createby { get; set; }
        public string companyid { get; set; }
        public string branchid { get; set; }
        public Nullable<double> openbal { get; set; }
        public Nullable<double> openingBal { get; set; }
        public string ChqNO { get; set; }
        public Nullable<bool> ChqOK { get; set; }
        public Nullable<double> opening_balance { get; set; }

    }
}