using System.Web;
using System.Web.Mvc;

namespace ECO3.Automation_TREE.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
