using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEverNote.BusınessLayer;
using MyEverNote.Entities;
using MyEverNote.WEBUI.Filters;
using MyEverNote.WEBUI.Models;

namespace MyEverNote.WEBUI.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class CategoriesController : Controller
    {
        private CategoryManager categorymanager = new CategoryManager();

        // GET: Categories
        public ActionResult Index()
        {
            return View(categorymanager.List());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categorymanager.Find(x=>x.Id==id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {
                categorymanager.Insert(category);
                Cache_Helper.RemoveCategoryCache();
                return RedirectToAction("Index","Categories");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category =categorymanager.Find(x=>x.Id==id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {
              
                Category cat = categorymanager.Find(x => x.Id == category.Id);
                cat.Title = category.Title;
                cat.Description = category.Description;


                categorymanager.Update(category);
                 Cache_Helper.RemoveCategoryCache();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categorymanager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categorymanager.Find(x => x.Id == id);
            categorymanager.Delete(category);


            return RedirectToAction("Index");
        }

       
    }
}
