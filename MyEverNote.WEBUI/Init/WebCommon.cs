using MyEverNote.Common;
using MyEverNote.Entities;
using MyEverNote.WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEverNote.WEBUI.Init
{
    public class WebCommon : ICommon
    {
       

        public string GetUsername()
        {

            EverNoteUser user = CurrentSession.CurrentUser;
            if (user != null)
            {
                return user.UserName;
            }
            else
            {
                return "system";

            }

         }
         
       
    }
}