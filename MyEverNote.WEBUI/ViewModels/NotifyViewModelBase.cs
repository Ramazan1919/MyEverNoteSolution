using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEverNote.WEBUI.ViewModels
{
    public class NotifyViewModelBase<T>
    {


        public List<T> ıtems { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeOut { get; set; }

        public NotifyViewModelBase()
        {
            Header = "Yönlendiriliyor";
            Title = "Geçersiz İşlem";
            IsRedirecting = true;
            RedirectingUrl = "/Home/Index";
            RedirectingTimeOut = 3000;
            ıtems = new List<T>();
        }





    }
}