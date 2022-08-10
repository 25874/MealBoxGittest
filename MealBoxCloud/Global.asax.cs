using MealBoxCloud.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace MealBoxCloud
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperProfile.Run();
        }
    }
}
