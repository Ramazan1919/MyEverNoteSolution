using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEverNote.Entities.ValueObject
{
    public class RegisterViewModels
    {
        [Required(ErrorMessage ="Geçerli Bir İsim Girin"),DisplayName("Kullanıcı adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "MAil adresi Boş Bırakılamaz"), DisplayName("E posta"),
        EmailAddress(ErrorMessage ="{0} alanı için geçerli bir eposta girin.")]
        public string  Email { get; set; }


        [Required(ErrorMessage = " Şifre Alanı Boş Bırakılamaz"), DisplayName("Şifre"),DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Şifre Alanı Boş Bırakılamaz"), DisplayName(" Tekrar Şifre"),DataType(DataType.Password),Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
        public string RePassword { get; set; }
    }
}