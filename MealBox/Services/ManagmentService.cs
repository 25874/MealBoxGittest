using MealBox;
using MealBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MealBoxes.Services
{
    public class ManagmentService
    {
        MealBoxesEntities Db = new MealBoxesEntities();
        public List<supplier> supplierList()
        {
            return Db.suppliers.ToList();
        }
        public supplier Getsupplier(int Id)
        {
            return Db.suppliers.Find(Id);
        }

        
        public void CreateSupplierAccount() 
        {

           var suheaddata   =  Db.Accounts.Where(w => w.headGeneratedIdCode == "003").Select(s => new 
           
           {
               headCode = s.headGeneratedIdCode,
               SubHeadCode = s.SubheadGeneratedIdCode,  
           }

           ).FirstOrDefault();

            var LastSupllier = Db.suppliers.OrderByDescending(o => o.supplierId).Select(s=> new
            { 
             SupplierId = s.supplierId,
             SupplierName = s.suppliername
            }
            ).FirstOrDefault();

            var AccountCode = "003";
            var Acoountcount = Db.Accounts.Count() + 1;
            AccountCode = AccountCode + Acoountcount;

            Account obj = new Account();
            obj.headGeneratedIdCode = suheaddata.headCode;
            obj.SubheadGeneratedIdCode = suheaddata.SubHeadCode;
            obj.PersonId = LastSupllier.SupplierId;
            obj.AccountName = LastSupllier.SupplierName;
            obj.CreateBy = 9;
            obj.AccountGeneratedCodeId = AccountCode;
            obj.CreatedAt = DateTime.Now;
            Db.Accounts.Add(obj);
            Db.SaveChanges();

        }

        public void CreateEmployeeAccount()
        {

            var suheaddata = Db.Accounts.Where(w => w.headGeneratedIdCode == "001").Select(s => new

            {
                headCode = s.headGeneratedIdCode,
                SubHeadCode = s.SubheadGeneratedIdCode,
            }

            ).FirstOrDefault();

            var LastCustomer = Db.Customers_.OrderByDescending(o => o.CustomerID).Select(s => new 
            {
            CustomerId = s.CustomerID,
            CustomerName = s.CustomerName
            }).FirstOrDefault();

            var AccountCode = "003";
            var Acoountcount = Db.Accounts.Count() + 1;
            AccountCode = AccountCode + Acoountcount;

            Account obj = new Account();
            obj.headGeneratedIdCode = suheaddata.headCode;
            obj.SubheadGeneratedIdCode = suheaddata.SubHeadCode;
            obj.PersonId = LastCustomer.CustomerId;
            obj.AccountName = LastCustomer.CustomerName;
            obj.CreateBy = 9;
            obj.AccountGeneratedCodeId = AccountCode;
            obj.CreatedAt = DateTime.Now;
            Db.Accounts.Add(obj);
            Db.SaveChanges();

        }

        public void CreateCustomerAccount()
        {

            var suheaddata = Db.Accounts.Where(w => w.headGeneratedIdCode == "003").Select(s => new

            {
                headCode = s.headGeneratedIdCode,
                SubHeadCode = s.SubheadGeneratedIdCode,
            }

            ).FirstOrDefault();

            var LastSupllier = Db.suppliers.OrderByDescending(o => o.supplierId).Select(s => s.supplierId).FirstOrDefault();

            var AccountCode = "003";
            var Acoountcount = Db.Accounts.Count() + 1;
            AccountCode = AccountCode + Acoountcount;

            Account obj = new Account();
            obj.headGeneratedIdCode = suheaddata.headCode;
            obj.SubheadGeneratedIdCode = suheaddata.SubHeadCode;
            obj.PersonId = LastSupllier;
            obj.CreateBy = 9;
            obj.AccountGeneratedCodeId = AccountCode;
            obj.CreatedAt = DateTime.Now;
            Db.Accounts.Add(obj);
            Db.SaveChanges();

        }



        public void AddCity(City Model)
        {
            try
            {
                Db.Cities.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        public void UpdateCity(City model)
        {
            try
            {
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public List<AreaModel> CityList()
        {
            var CityList = Db.Cities.ToList();
            var ProvinceList = Db.Provinces.ToList();

            var query = (from a in CityList
                         join b in ProvinceList
                         on a.PrivinceId equals b.PrivinceId
                         select new AreaModel
                         {
                             CityId = a.CityId,
                             CityName = a.CityName,
                             ProvinceName = b.ProvinceName

                         }).ToList();
            return query;
        }
        public City GetCity(int Id)
        {
            return Db.Cities.Find(Id);
        }

        public Province GetProvince(int Id)
        {
            return Db.Provinces.Find(Id);

        }

        public object GetSaleDiscount(int id) 
        {
            var query = Db.Customers_.Where(w => w.CustomerID == id).Select(s => new
            {
                SaleDes = s.saleper

            }).FirstOrDefault();

            return query;
        }

        public List<Province> ProvinceList()
        {
            return Db.Provinces.ToList();
        }

        public void AddProvince(Province Model)
        {
            try
            {
                Db.Provinces.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        public void UpdateProvince(Province model)
        {
            try
            {
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Addsupplier(supplier Model)
        {
            try
            {
                Db.suppliers.Add(Model);
                Db.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
        public void Updatesupplier(supplier model)
        {
            try
            {
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        public List<tbl_area> AreaList()
        {
            return Db.tbl_area.ToList();
        }
        public tbl_area GetArea(int Id)
        {
            return Db.tbl_area.Find(Id);
        }

        public void AddArea(tbl_area Model)
        {
            try
            {
                Db.tbl_area.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
        public void UpdatetblArea(tbl_area Model)
        {
            Db.Entry(Model).State = EntityState.Modified;
            Db.SaveChanges();
        }


        public List<Customers_> CustomersList()
        {
            return Db.Customers_.ToList();
        }
        public Customers_ GetCustomers(int Id)
        {
            return Db.Customers_.Find(Id);
        }

        public void AddCustomers(Customers_ Model)
        {
            try
            {
                Db.Customers_.Add(Model);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
        public void AddBank(tbl_CashBnk Model) 
        {
            Db.tbl_CashBnk.Add(Model);
            Db.SaveChanges();
        }
        public void UpdateBank(tbl_CashBnk model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }
        public List<tbl_CashBnk> BankList()
        {
            return Db.tbl_CashBnk.ToList();
        }
        public tbl_CashBnk GetBank(int Id)
        {
            return Db.tbl_CashBnk.Find(Id);
        }
        public void UpdateCustomers(Customers_ model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public List<tbl_employee> EmployeeList()
        {
            return Db.tbl_employee.ToList();
        }
        public tbl_employee GetEmployee(int Id)
        {
            return Db.tbl_employee.Find(Id);
        }

        public void AddEmployee(tbl_employee Model)
        {            
                Db.tbl_employee.Add(Model);
                Db.SaveChanges();
        }
        public void UpdateEmployee(tbl_employee model)
        {
            Db.Entry(model).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}