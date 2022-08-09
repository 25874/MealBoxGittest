using MealBox;
using MealBoxCloud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MealBoxCloud.Services
{
    public class ManagmentService
    {
        MealBoxesEntities Db = new MealBoxesEntities();
        public List<supplier> supplierList()
        {
            return Db.suppliers.OrderByDescending(o=> o.supplierId).ToList();
        }
        public supplier Getsupplier(int Id)
        {
            return Db.suppliers.Find(Id);
        }

        public List<City> GetCityList(int id)
        {
            var data = Db.Cities.Where(w => w.PrivinceId == id)

            .ToList();
            return data;
        }


        public void CreateSupplierAccount()
        {

            var suheadKey = Db.tbl_Prefernce.Where(w => w.headValue == "Supplier").Select(s => s.Headkey).FirstOrDefault();

            var HeadKey = Db.SubHeads.Where(w => w.SubHeadGeneratedIDCode == suheadKey.ToString()).Select(s => s.HeadGeneratedIdCode).FirstOrDefault();


            var LastSupllier = Db.suppliers.OrderByDescending(o => o.supplierId).Select(s => new
            {
                SupplierId = s.supplierId,
                SupplierName = s.suppliername
            }
            ).FirstOrDefault();

            var AccountCode = "003";
            var Acoountcount = Db.Accounts.Count() + 1;
            AccountCode = AccountCode + Acoountcount;

            Account obj = new Account();
            obj.headGeneratedIdCode = HeadKey;
            obj.SubheadGeneratedIdCode = suheadKey;
            obj.PersonId = LastSupllier.SupplierId;
            obj.AccountName = LastSupllier.SupplierName;
            obj.CreateBy = 9;
            obj.AccountGeneratedCodeId = AccountCode;
            obj.CreatedAt = DateTime.Now;
            Db.Accounts.Add(obj);
            Db.SaveChanges();

        }



        public void AssignPage(LoginSinUpModel Model) 
        {

            tbl_UserMenu obj = new tbl_UserMenu();
            obj.MenuId = Model.Module;
            obj.UserId = Model.EmployeeId;
            obj.UserName = Model.EmployeeName;
            obj.CreateBy = 1;
            obj.IsActive = Model.IsActive;
            Db.tbl_UserMenu.Add(obj);
            Db.SaveChanges();
        }


        public void CreateEmployeeAccount()
        {

            var suheadKey = Db.tbl_Prefernce.Where(w => w.headValue == "employee").Select(s => s.Headkey).FirstOrDefault();

            var HeadKey = Db.SubHeads.Where(w => w.SubHeadGeneratedIDCode == suheadKey.ToString()).Select(s => s.HeadGeneratedIdCode).FirstOrDefault();

            var LastCustomer = Db.tbl_employee.OrderByDescending(o => o.employeeID).Select(s => new
            {
                EmployeeId = s.employeeID,
                EmployeeName = s.employeeName
            }).FirstOrDefault();

            var AccountCode = "003";
            var Acoountcount = Db.Accounts.Count() + 1;
            AccountCode = AccountCode + Acoountcount;

            Account obj = new Account();
            obj.headGeneratedIdCode = HeadKey;
            obj.SubheadGeneratedIdCode = suheadKey.ToString();
            obj.PersonId = LastCustomer.EmployeeId;
            obj.AccountName = LastCustomer.EmployeeName;
            obj.CreateBy = 9;
            obj.AccountGeneratedCodeId = AccountCode;
            obj.CreatedAt = DateTime.Now;
            Db.Accounts.Add(obj);
            Db.SaveChanges();

        }

        public object Booker() 
        {
            var BookerList = Db.tbl_User.Where(w => w.UserTypeId == 2).Select(s => new
            {

                Bookerid = s.EmployeeId,
                BookerName = s.Name

            }).ToList();
            return BookerList;
        }


        public void CreateCustomerAccount()
        {

            var suheadKey = Db.tbl_Prefernce.Where(w => w.headValue == "Customer").Select(s => s.Headkey).FirstOrDefault();

            var HeadKey = Db.SubHeads.Where(w => w.SubHeadGeneratedIDCode == suheadKey.ToString()).Select(s => s.HeadGeneratedIdCode).FirstOrDefault();

            var LastCustomer = Db.Customers_.OrderByDescending(o => o.CustomerID).Select(s => new
            {
                CustomerId = s.CustomerID,
                CustomerName = s.CustomerName
            }).FirstOrDefault();

            var AccountCode = "003";
            var Acoountcount = Db.Accounts.Count() + 1;
            AccountCode = AccountCode + Acoountcount;

            Account obj = new Account();
            obj.headGeneratedIdCode = HeadKey;
            obj.SubheadGeneratedIdCode = suheadKey;
            obj.PersonId = LastCustomer.CustomerId;
            obj.AccountName = LastCustomer.CustomerName;
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

                         }).OrderByDescending(o=> o.CityId).ToList();
           
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
            return Db.Provinces.OrderByDescending(o=> o.PrivinceId).ToList();
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
            return Db.tbl_area.OrderByDescending(o=> o.areaid).ToList();
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
            return Db.Customers_.OrderByDescending(o=> o.CustomerID).ToList();
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
            return Db.tbl_CashBnk.OrderByDescending(o => o.CashBnk_id).ToList();
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
            return Db.tbl_employee.OrderByDescending(o=> o.employeeID).ToList();
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