using AutoMapper;
using MealBoxCloud.Models;

namespace MealBoxCloud.Infrastructure
{
    public class AutoMapperProfile
    {
        public static IMapper Mapper { get; set; }

        public static void Run()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductModel>().ReverseMap();
                cfg.CreateMap<supplier, SupplierModel>().ReverseMap();
                cfg.CreateMap<tbl_employee, EmployeeModel>().ReverseMap();
                cfg.CreateMap<Customers_, CustomerModel>().ReverseMap();
                cfg.CreateMap<tbl_producttype, ProductModel>().ReverseMap();
                cfg.CreateMap<MPurchase, PurchaseModel>().ReverseMap();
                cfg.CreateMap<tbl_MSal, SalesModel>().ReverseMap();
                cfg.CreateMap<tbl_area, AreaModel>().ReverseMap();
                cfg.CreateMap<tbl_CashBnk, BankModel>().ReverseMap();
                cfg.CreateMap<AccountHead, AccountHeadMOdel>().ReverseMap();
                cfg.CreateMap<SubHead, SubHeadModel>().ReverseMap();
                cfg.CreateMap<Account, SubHeadCategoriesModel>().ReverseMap();
                cfg.CreateMap<stockIn, InventoryModel>().ReverseMap();
                cfg.CreateMap<Province, AreaModel>().ReverseMap();
                cfg.CreateMap<City, AreaModel>().ReverseMap();
                cfg.CreateMap<tbl_WareHouse, WareHouseModel>().ReverseMap();
                cfg.CreateMap<tbl_WareHouseInventory, WareHouseModel>().ReverseMap();
                cfg.CreateMap<Mpos, Postable>().ReverseMap();
            });

            Mapper = config.CreateMapper();
        }

    }
}