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
    [Table("Notes")]
   public  class Note :MyEntityBase
    {
        [Required,StringLength(50)]
        [DisplayName("Başlık")]
        public string Title { get; set; }

        [Required, StringLength(20000)]
        [DisplayName("İçerik")]
        public string  Text { get; set; }


        [DisplayName("Taslak Mı ?")]
        public bool IsDraft { get; set; }
        [DisplayName("Beğeni Sayısı")]
        public int LikeCount { get; set; }

        [DisplayName("Kategori")]
        public int CategoryId { get; set; }


        [StringLength(50), ScaffoldColumn(false)]
        public string NoteImageFilename { get; set; }


        public virtual EverNoteUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Liked> Likes { get; set; }



        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();

        }

    }
}
