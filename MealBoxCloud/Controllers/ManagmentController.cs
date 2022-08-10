using AutoMapper;
using MealBoxCloud.Infrastructure;
using MealBoxCloud.Models;
using MealBoxCloud.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MealBoxCloud.Controllers
{
    public class ManagmentController : Controller
    {
        // GET: Managment
        private readonly IMapper _mapper;

        MealBoxesEntities Db = new MealBoxesEntities();

        private readonly ManagmentService managmentService;
        public ManagmentController()
        {
            managmentService = new ManagmentService();
            _mapper = AutoMapperProfile.Mapper;
        }
        public ActionResult AddSupplier(int? id)
        {
            var CitiesList = managmentService.CityList();
            ViewBag.City_ = new SelectList(CitiesList, "CityId", "CityName");
            if (id > 0)
            {
                var data = managmentService.Getsupplier(id.Value);
                var ModalData = _mapper.Map<SupplierModel>(data);
                return View(ModalData);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplier(SupplierModel Model)
        {
            var CitiesList = managmentService.CityList();
            ViewBag.City_ = new SelectList(CitiesList, "CityId", "CityName");

            var id = Model.supplierId;
            if (id == 0)
            {

                var Modeldata = _mapper.Map<supplier>(Model);
                managmentService.Addsupplier(Modeldata);
                managmentService.CreateSupplierAccount();
            }
            else
            {
                var modeldata = _mapper.Map<supplier>(Model);
                managmentService.Updatesupplier(modeldata);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddArea(int? id)
        {
            var ProvinceList = managmentService.ProvinceList();
            ViewBag.ProvinceIdFk = new SelectList(ProvinceList, "PrivinceId", "ProvinceName");

            ViewBag.CityIdFk = new SelectList("", "");

            if (id > 0)
            {
                var CitiesList = managmentService.CityList();

                var data = managmentService.GetArea(id.Value);
                ViewBag.ProvinceIdFk = new SelectList(ProvinceList, "PrivinceId", "ProvinceName", data.ProvinceIdFk);
                ViewBag.CityIdFk = new SelectList(CitiesList, "CityId", "CityName", data.CityIdFk);

                var ModalData = _mapper.Map<AreaModel>(data);

                return View(ModalData);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddArea(AreaModel Model)
        {
            var id = Model.areaid;
            if (id == 0)
            {
                var Modeldata = _mapper.Map<tbl_area>(Model);
                managmentService.AddArea(Modeldata);
            }
            else
            {
                var modeldata = _mapper.Map<tbl_area>(Model);
                managmentService.UpdatetblArea(modeldata);
            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaList()
        {
            var List = managmentService.AreaList();
            var AreaModel = _mapper.Map<List<AreaModel>>(List);





            return View(AreaModel);

        }




        public ActionResult Cityddl(int id)
        {
            var data = managmentService.GetCityList(id);



            var dataGet = data
            .Select(s => new
            {


                CityId = s.CityId,
                City = s.CityName



            }
        ).ToList();

            return Json(dataGet, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddBank(int? id)
        {
            if (id > 0)
            {
                var data = managmentService.GetBank(id.Value);

                var ModalData = _mapper.Map<BankModel>(data);

                return View(ModalData);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddBank(BankModel Model)
        {
            var id = Model.CashBnk_id;
            if (id == 0)
            {
                var Modeldata = _mapper.Map<tbl_CashBnk>(Model);
                managmentService.AddBank(Modeldata);
            }
            else
            {
                var modeldata = _mapper.Map<tbl_CashBnk>(Model);
                managmentService.UpdateBank(modeldata);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult BankList()
        {
            var List = managmentService.BankList();
            var Model = _mapper.Map<List<BankModel>>(List);
            return View(Model);
        }


        public ActionResult PagesAssign()
        {
            var EmployeeList = Db.tbl_employee.ToList();
            ViewBag.EmployeeId = new SelectList(EmployeeList, "employeeID", "employeeName");

            var ModuleList = Db.tbl_Menu.ToList();
            ViewBag.Module = new SelectList(ModuleList, "MenuId", "Title");

            var PagesList = Db.tbl_SubMenu.ToList();
            ViewBag.pages = new SelectList(PagesList, "SubMenuId", "SubMenuNam");

            return View();
        }

        [HttpPost]
        public ActionResult PagesAssign(LoginSinUpModel Model)
        {
            managmentService.AssignPage(Model);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        public ActionResult SupplierList()
        {
            var List = managmentService.supplierList();
            var Model = _mapper.Map<List<SupplierModel>>(List);
            return View(Model);
        }

        public ActionResult AddCustomer(int? id)
        {
            var CitiesList = managmentService.CityList();
            ViewBag.city_ = new SelectList(CitiesList, "CityId", "CityName");

            var AreaList = managmentService.AreaList();
            ViewBag.Area = new SelectList(AreaList, "areaid", "area_");

            if (id > 0)
            {
                var data = managmentService.GetCustomers(id.Value);
                var ModalData = _mapper.Map<CustomerModel>(data);
                return View(ModalData);
            }


            return View();
        }


        public ActionResult SubMenuddl(int id)
        {
            var SubMenuList = Db.tbl_SubMenu.Where(w => w.MenuId == id).Select(s => new
            {
                SubMenuId = s.SubMenuId,
                SubMenuName = s.SubMenuNam,

            }).ToList();

            return Json(SubMenuList, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult AddCustomer(CustomerModel Model)
        {
            var id = Model.CustomerID;
            if (id == 0)
            {
                var Modeldata = _mapper.Map<Customers_>(Model);
                managmentService.AddCustomers(Modeldata);
                managmentService.CreateCustomerAccount();

            }
            else
            {
                var modeldata = _mapper.Map<Customers_>(Model);
                managmentService.UpdateCustomers(modeldata);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);

        }

        public ActionResult CustomerList()
        {
            var List = managmentService.CustomersList();
            var Model = _mapper.Map<List<CustomerModel>>(List);
            return View(Model);
        }

        public ActionResult AddEmployee(int? id)
        {
            if (id > 0)
            {
                var data = managmentService.GetEmployee(id.Value);
                var ModalData = _mapper.Map<EmployeeModel>(data);
                return View(ModalData);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel Model)
        {
            var id = Model.employeeID;
            if (id == 0)
            {
                var Modeldata = _mapper.Map<tbl_employee>(Model);
                managmentService.AddEmployee(Modeldata);
                managmentService.CreateEmployeeAccount();
            }
            else
            {
                var modeldata = _mapper.Map<tbl_employee>(Model);
                managmentService.UpdateEmployee(modeldata);
            }
            return View();
        }

        public ActionResult CustomerDiscount(int? id)
        {
            var Data = Db.Customers_.Where(w => w.CustomerID == id).Select(s => s.Discount).FirstOrDefault();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignSalesman(int? id)
        {
            var BookerList = Db.tbl_User.Where(w => w.UserTypeId == 2).Select(s => new
            {

                Bookerid = s.EmployeeId,
                BookerName = s.Name

            }).ToList();

            var SalesmanList = Db.tbl_User.Where(w => w.UserTypeId == 3).Select(s => new
            {

                Salesmanid = s.EmployeeId,
                SalesmanName = s.Name

            }).ToList();

            ViewBag.booker = new SelectList(BookerList, "Bookerid", "BookerName");


            ViewBag.Salesman = new SelectList(SalesmanList, "Salesmanid", "SalesmanName");


            return View();
        }

        [HttpPost]
        public ActionResult AssignSalesman(EmployeeModel Model)
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            var List = managmentService.EmployeeList();
            var Model = _mapper.Map<List<EmployeeModel>>(List);
            return View(Model);
        }

        public ActionResult AddProvince(int? id)
        {
            if (id > 0)
            {
                var data = managmentService.GetProvince(id.Value);
                var ModalData = _mapper.Map<AreaModel>(data);
                return View(ModalData);
            }

            return View();

        }

        [HttpPost]
        public ActionResult AddProvince(AreaModel Model)
        {
            var id = Model.PrivinceId;
            if (id == 0)
            {
                var Modeldata = _mapper.Map<Province>(Model);
                managmentService.AddProvince(Modeldata);
            }
            else
            {
                var modeldata = _mapper.Map<Province>(Model);
                managmentService.UpdateProvince(modeldata);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);

        }
        public ActionResult ProvinceList()
        {
            var List = managmentService.ProvinceList();
            var Model = _mapper.Map<List<AreaModel>>(List);
            return View(Model);
        }

        public ActionResult AddCity(int? id)
        {
            var ProvinceList = managmentService.ProvinceList();
            ViewBag.PrivinceId = new SelectList(ProvinceList, "PrivinceId", "ProvinceName");

            if (id > 0)
            {
                var data = managmentService.GetCity(id.Value);
                var ModalData = _mapper.Map<AreaModel>(data);
                return View(ModalData);
            }
            return View();

        }

        [HttpPost]
        public ActionResult AddCity(AreaModel Model)
        {
            var id = Model.CityId;

            if (id == 0)
            {
                var Modeldata = _mapper.Map<City>(Model);
                managmentService.AddCity(Modeldata);
            }
            else
            {
                var modeldata = _mapper.Map<City>(Model);
                managmentService.UpdateCity(modeldata);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaleDis(int id)
        {
            var data = managmentService.GetSaleDiscount(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CityList()
        {
            var Model = managmentService.CityList();
            return View(Model);
        }
    }
}