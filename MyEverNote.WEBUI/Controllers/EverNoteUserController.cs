using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEverNote.BusınessLayer;
using MyEverNote.BusınessLayer.Results;
using MyEverNote.Entities;
using MyEverNote.WEBUI.Filters;

namespace MyEverNote.WEBUI.Controllers
{

    [Auth]
    [AuthAdmin]
    [Exc]
    public class EverNoteUserController : Controller
    {
        private UserManager userManager = new UserManager();

        // GET: EverNoteUser
        public ActionResult Index()
        {
            return View(userManager.List());
        }

        // GET: EverNoteUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EverNoteUser everNoteUser = userManager.Find(x=>x.Id==id.Value);
            if (everNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everNoteUser);
        }

        // GET: EverNoteUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EverNoteUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( EverNoteUser everNoteUser)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {

                BusınessLayerResult<EverNoteUser> res = userManager.Insert(everNoteUser);

                if (res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(everNoteUser);
                }
                return RedirectToAction("Index","EverNoteUser");
            }

            return View(everNoteUser);
        }

        // GET: EverNoteUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EverNoteUser everNoteUser = userManager.Find(x => x.Id == id.Value);
            if (everNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everNoteUser);
        }

        // POST: EverNoteUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( EverNoteUser everNoteUser)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            { 

                //Yapılacak

                BusınessLayerResult<EverNoteUser> res = userManager.Update(everNoteUser);

                if (res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(everNoteUser);
                }


                return RedirectToAction("Index");
            }
            return View(everNoteUser);
        }

        // GET: EverNoteUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EverNoteUser everNoteUser = userManager.Find(x => x.Id == id.Value);
            if (everNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everNoteUser);
        }

        // POST: EverNoteUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EverNoteUser everNoteUser = userManager.Find(x => x.Id == id); ;
            userManager.Delete(everNoteUser);
            userManager.Save();
            return RedirectToAction("Index");
        }

    }
}
