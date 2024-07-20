using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarShopAPI.Helpers
{
  [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
  public class Administrator : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var cookies = context.HttpContext.Request.Cookies;
      var nameCookie = cookies["username"];
      var roleCookie = cookies["role"];

      if (nameCookie == null || nameCookie != Utils.GetAdminUser().Username)
      {
        context.Result = new UnauthorizedResult();
        return;
      }

      if (roleCookie == null || roleCookie != "admin")
      {
        context.Result = new UnauthorizedResult();
        return;
      }

      base.OnActionExecuting(context);
    }
  }
}