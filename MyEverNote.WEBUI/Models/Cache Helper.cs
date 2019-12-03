using MyEverNote.BusınessLayer;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MyEverNote.WEBUI.Models
{
    public class Cache_Helper
    {


        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                CategoryManager categoryManager = new CategoryManager();
                result = categoryManager.List();
                  WebCache.Set("category-cache", result, 20,true);
            }
            return result;
        }

        public static  void RemoveCategoryCache()
        {

            Remove("category-cache");
        }
        public static void Remove(string key)
        {

            WebCache.Remove(key);
        }

    }
}