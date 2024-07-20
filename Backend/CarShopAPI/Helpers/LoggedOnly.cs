using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarShopAPI.Helpers
{
  [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
  public class LoggedOnly : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var cookies = context.HttpContext.Request.Cookies;
      var nameCookie = cookies["username"];
      var roleCookie = cookies["role"];

      if (nameCookie == null || roleCookie == null)
      {
        context.Result = new UnauthorizedResult();
        return;
      }

      base.OnActionExecuting(context);
    }
  }
}
