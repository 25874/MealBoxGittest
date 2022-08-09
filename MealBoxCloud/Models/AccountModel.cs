using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealBoxCloud.Models
{
    public class AccountModel
    {
        public List<AccountHeadMOdel> accountHeads { get; set; }

        public List<SubHeadModel> SubHeads { get; set; }

        public List<SubHeadCategoriesModel> Accounttbl { get; set; }

        public int AccountIDv { get; set; }
        public string AccounId { get; set; }

        public int SubHead { get; set; }
        public string Type { get; set; }
        public string SubHeadName { get; set; }
        public string AccountNamev { get; set; }
        public int AccountIDFkv { get; set; }
        public string SubAccountId { get; set; }
        public string AccountName { get; set; }
        public int AccountIDFkv2 { get; set; }
        public int SubAccounthIdv { get; set; }
        public string SubAccountName { get; set; }
        public int SubAccountCategoriesidv { get; set; }

        public int SubAccountCategorieshidv { get; set; }
        public string SubAccountCategoriesNamec { get; set; }
        public string AccountDate { get; set; }
        public int PayAccount { get; set; }
        
        public int RecAccount { get; set; }
        public int VoucherType { get; set; }
        public int paymentType { get; set; }
        public string AmountPaid { get; set; }
        public string PreviousBalance { get; set; }
        public string Expenseremarks { get; set; }
        public int SubAccountIdfkv { get; set; }

        public string HeadGeneratedID { get; set; }

        public string  SubHeadGeneratedID { get; set; }

        public string SubHeadCategoriesGeneratedID { get; set; }
    }

    public class AccountHeadMOdel
    {
        public string HeadGeneratedIdCode { get; set; }
        public string HeadName { get; set; }

        //public DateTime CreatedAt { get; set; }        
        //public string CreatedBy { get; set; }
        //public string HeadKey { get; set; }
        //public string CompanyId { get; set; }
        //public string BranchId { get; set; }
    }

    public class SubHeadModel
    {
        public int SubHeadID { get; set; }
        public string SubHeadName { get; set; }
        public string HeadGeneratedIdCodeID { get; set; }

        public string SubHeadGeneratedIdCode { get; set; }
        public string HeadName { get; set; }

        //public DateTime CreatedAt { get; set; }
        //public string  HeadGeneratedID { get; set; }
        //public string CreatedBy { get; set; }
        //public string HeadKey { get; set; }
        //public string CompanyId { get; set; }
        //public string BranchId { get; set; }
    }

    public class SubHeadCategoriesModel
    {
        public int Account { get; set; }
        public string AccountGeneratedCodeeID { get; set; }
        public string AccountName { get; set; }
        public string HeadName { get; set; }
        public string SubHeadName { get; set; }
        public string SubHeadGeneratedID { get; set; }
        public string HeadGeneratedID { get; set; }
        
        public int SubHeadCategorieId { get; set; }

    }
}