using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities
{

    [Table("EvernoteUsers")]
   public  class EverNoteUser :MyEntityBase
    {
        [StringLength(50)]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Soyisim")]
        public string Surname { get; set; }

        [Required, StringLength(50)]
        [DisplayName("E-Posta")]
        public string Email { get; set; }

        [Required, StringLength(50)]
        [DataType("Password")]
        [DisplayName("Şifre")]
        public string Password { get; set; }



        [StringLength(50),ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }
        [DisplayName(" Aktif Mi?")]
        public bool IsActıve { get; set; }

        [Required, ScaffoldColumn(false)]
        public Guid ActıveGuıd { get; set; }
        [DisplayName(" Admin  Mi?")]
        public bool IsAdmın { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

         
    }
}
