﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities
{

    [Table("Comments")]
    public class Comment:MyEntityBase
    {
        [Required,StringLength(300)]
        public string Text { get; set; }


        public Note Note { get; set; }
        public EverNoteUser Owner { get; set; }



    }
}
