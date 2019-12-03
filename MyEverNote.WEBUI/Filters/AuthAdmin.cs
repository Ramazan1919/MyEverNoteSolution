using MyEverNote.WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEverNote.WEBUI.Filters
{
    public class AuthAdmin : FilterAttribute , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
           if(CurrentSession.CurrentUser!=null && CurrentSession.CurrentUser.IsAdmın==false )
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");

            }
        }
    }
}