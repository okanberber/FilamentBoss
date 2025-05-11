using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using FilamentBossWebApp.Models;

namespace FilamentBossWebApp.Areas.ManagerPanel.Filters
{
    public class ManagerLoginRequiredFilterAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        FilamentBossDbModel db = new FilamentBossDbModel();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["ManagerSession"])))
            {
                if (filterContext.HttpContext.Request.Cookies["ManagerCookie"] !=null)
                {
                    HttpCookie SavedCookie = filterContext.HttpContext.Request.Cookies["ManagerCookie"];
                    string mail = SavedCookie.Values["mail"];
                    string password = SavedCookie.Values["password"];

                    Manager m = db.Managers.FirstOrDefault(x => x.Mail == mail && x.Password == password);
                    if (m != null)
                    {
                        if (m.IsActive)
                        {
                            filterContext.HttpContext.Session["ManagerSession"] = m;  
                        }
                    }
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/ManagerPanel/Login/Index");
            }
        }
    }
}