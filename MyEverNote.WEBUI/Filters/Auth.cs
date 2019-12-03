using MyEverNote.WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEverNote.WEBUI.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.CurrentUser == null)
            {

                filterContext.Result = new RedirectResult("/Home/Login");
            }

        }
    }
}