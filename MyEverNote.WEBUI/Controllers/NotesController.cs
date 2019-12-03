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
    [Exc]
    public class NotesController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categorymanager = new CategoryManager();
        private LikedManager likedManger = new LikedManager();
        // GET: Notes
        [Auth]
        public ActionResult Index()
        {
            var notes = noteManager.ListQueryable().Include("Category").Include("Owner").Where(
                 x => x.Owner.Id == CurrentSession.CurrentUser.Id).OrderByDescending(x => x.ModifiedOn);
            if (CurrentSession.CurrentUser.IsAdmın)
            {
               notes = noteManager.ListQueryable().Include("Category").Include("Owner").OrderByDescending(x => x.ModifiedOn);
            }
            return View(notes.ToList());
        }

        // GET: Notes/Details/5
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        [Auth]
        public ActionResult MyLikedNotes()
        {
            var liked =likedManger.ListQueryable().Include("LikedUser").Include("Note").Where(
               x => x.LikedUser.Id == CurrentSession.CurrentUser.Id).Select(x=>x.Note).Include("Category").Include("Owner").OrderByDescending(x=>x.ModifiedOn);




            return View("Index",liked.ToList());
        }

        // GET: Notes/Create
        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Cache_Helper.GetCategoriesFromCache(), "Id", "Title");

            
            return View();
        }

        // POST: Notes/Create
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Note note, HttpPostedFileBase ProfileImage)
        {


            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {


                if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {

                    string filename = $"user_{note.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    note.NoteImageFilename = filename;



                }



                note.Owner = CurrentSession.CurrentUser;
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(Cache_Helper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        // GET: Notes/Edit/5
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(Cache_Helper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        // POST: Notes/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth]
        public ActionResult Edit( Note note, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            if (ModelState.IsValid)
            {

                if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {

                    string filename = $"user_{note.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    note.NoteImageFilename = filename;



                }




                Note db_note = noteManager.Find(x => x.Id == note.Id);

                db_note.IsDraft = note.IsDraft;
                db_note.CategoryId = note.CategoryId;
                db_note.Text = note.Text;
                db_note.Title = note.Title;
                db_note.NoteImageFilename = note.NoteImageFilename;
                noteManager.Update(db_note);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(Cache_Helper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);





        }

        // GET: Notes/Delete/5
        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.Find(x => x.Id == id);
            noteManager.Delete(note);
            noteManager.Save();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {

            if (CurrentSession.CurrentUser != null)
            {
                List<int> likedNoteIds = likedManger.List(
                    x => x.LikedUser.Id == CurrentSession.CurrentUser.Id && ids.Contains(x.Note.Id)).Select(
                    x => x.Note.Id).ToList();

                return Json(new { result = likedNoteIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }

         

        }


        public ActionResult SetLikeState(int noteid,bool liked)
        {

            int res = 0;
            Liked like = likedManger.Find(x => x.Note.Id == noteid && x.LikedUser.Id == CurrentSession.CurrentUser.Id);
            Note note = noteManager.Find(x => x.Id == noteid); 

            if(like!=null && liked == false)
            {

                res = likedManger.Delete(like);
            }else if (like == null && liked==true)
            {
                res = likedManger.Insert(new Liked()
                {
                    LikedUser = CurrentSession.CurrentUser,
                    Note = note
                });

            }

            if (res > 0)
            {
                if (liked)
                {
                    note.LikeCount++;
                }
                else
                {

                    note.LikeCount--;
                }

                noteManager.Update(note);

                return Json(new { hassError = false, errormessage = string.Empty , result = note.LikeCount });
            }



            return Json(new { hassError = true, errormessage = "Beğenme işlemi gerçekleştirilmedi", result = note.LikeCount });
        }

      
        public ActionResult ShowNotes(int ? id)
        {

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id.Value);

            if (note == null)
            {


                return HttpNotFound();
            }
            else
            {

                return PartialView("_Note2", note);
            }



        }
    }
}
