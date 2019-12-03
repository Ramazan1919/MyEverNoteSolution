﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities.Messages
{
     public class ErrorMessageObject
    {
        public ErrorMessageCode Code { get; set; }
        public string Message { get; set; }
        public object ModalState { get; set; }
    }
}
