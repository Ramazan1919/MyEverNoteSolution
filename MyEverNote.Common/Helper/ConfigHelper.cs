﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Common.Helper
{
    public class ConfigHelper
    {
        public static T Get<T> (string key)
        {

            var model= (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            return  model;

        }
    }
}
