using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEverNote.Entities.ValueObject
{
    public class LoginViewModels
    {
        [DisplayName("Kullanıcı Adı") ,Required(ErrorMessage ="{0} alanı boş bırakılamaz" )]
        public string UserName { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş bırakılamaz"),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}