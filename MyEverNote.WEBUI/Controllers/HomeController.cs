using MyEverNote.BusınessLayer;
using MyEverNote.BusınessLayer.Results;
using MyEverNote.Entities;
using MyEverNote.Entities.Messages;
using MyEverNote.Entities.ValueObject;
using MyEverNote.WEBUI.Filters;
using MyEverNote.WEBUI.Models;
using MyEverNote.WEBUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace MyEverNote.WEBUI.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Category Controllerdan gelen view talebi ve model
            //if (TempData["mm"] != null)
            //{

            //    return View(TempData["mm"] as List<Note>);
            //}


            NoteManager nm = new NoteManager();
            
            return View(nm.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());
            //  return View(nm.ListQueryable().OrderByDescending(x=>x.ModifiedOn).ToList());
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            CategoryManager cm = new CategoryManager();

            Category cat = cm.Find(x => x.Id == id.Value);


            if (cat == null)
            {
                return HttpNotFound();

            }



            return View("Index", cat.Notes.Where(x=>x.IsDraft==false).OrderByDescending(x => x.ModifiedOn).ToList());//Index Vievina cat.Not Modelini Yollar


        }

        public ActionResult MostLiked()
        {

            NoteManager note = new NoteManager();


            return View("Index",note.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {

            return View();
        }

        [Auth]
        public ActionResult ShowProfile()
        {
           

            UserManager userManager = new UserManager();

            BusınessLayerResult<EverNoteUser> res = userManager.GetUserById(CurrentSession.CurrentUser.Id);



            if (res.Errors.Count > 0)
            {
                //Kullanıcı Hata Ekranına Yönlendirilir;

                ErrorViewModel model = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    ıtems = res.Errors,

                };

                return View("Error", model);

            }

            return View(res.Result);
        }

        [Auth]
        public ActionResult EditProfile()
        {


           

            UserManager userManager = new UserManager();

            BusınessLayerResult<EverNoteUser> res = userManager.GetUserById(CurrentSession.CurrentUser.Id);



            if (res.Errors.Count > 0)
            {
                //Kullanıcı Hata Ekranına Yönlendirilir;

                ErrorViewModel model = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    ıtems = res.Errors,

                };

                return View("Error", model);

            }

            return View(res.Result);
        }
        [Auth]
        [HttpPost]
        public ActionResult EditProfile(EverNoteUser model , HttpPostedFileBase  ProfileImage)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {

                if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {

                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    model.ProfileImageFilename = filename;


                  
                }

                UserManager um = new UserManager();
                BusınessLayerResult<EverNoteUser> res = um.UpdateUser(model);

                if (res.Errors.Count > 0)
                {
                    //Kullanıcı Hata Ekranına Yönlendirilir;

                    ErrorViewModel errorViewModel = new ErrorViewModel()
                    {
                        Title = "Güncelleme Başarısız",
                        ıtems = res.Errors,
                        RedirectingUrl = "/Home/EditProfile"

                    };

                    return View("Error", errorViewModel);

                }
                CurrentSession.Set<EverNoteUser>("login", res.Result);

                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }
        [Auth]
        public ActionResult DeleteProfile()
        {
          

            UserManager um = new UserManager();
            BusınessLayerResult<EverNoteUser> res = um.DeleteUserById(CurrentSession.CurrentUser.Id);



            if (res.Errors.Count > 0)
            {
                //Kullanıcı Hata Ekranına Yönlendirilir;

                ErrorViewModel errorViewModel = new ErrorViewModel()
                {
                    Title = "Silme Başarısız",
                    ıtems = res.Errors,
                    RedirectingUrl = "/Home/ShowProfile"

                };

                return View("Error", errorViewModel);

            }



            Session.Clear();

            return RedirectToAction("Index");
        }


      

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModels loginViewModels)
        {

            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                BusınessLayerResult<EverNoteUser> res = um.LoginUser(loginViewModels);


                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActıve) != null)
                    {

                        ViewBag.SetLink = "http://Home/Activate/1234-456-789";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(loginViewModels);
                }

                CurrentSession.Set<EverNoteUser>("login",res.Result);
                return RedirectToAction("Index");

            }

            return View(loginViewModels);
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModels registerViewModels)
        {
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                BusınessLayerResult<EverNoteUser> res = um.RegisterUser(registerViewModels);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(registerViewModels);
                }
                else
                {
                    //OkViewModel model = new OkViewModel()
                    //{
                    //    Title = "Yönlendiriliyor..",
                    //    RedirectingUrl = "/Home/Login",
                    //    RedirectingTimeOut = 2000,


                    //};
                    //model.ıtems.Add("Lütfen E-Posta adresinize yolladığımız linke tıklayarak hesabınızı aktivite ediniz..");
                    return RedirectToAction("Login", "Home");

                }

               
            }
            return View(registerViewModels);

        }


        

        public ActionResult UserActivate(Guid activae_id)
        {

            UserManager eum = new UserManager();

            BusınessLayerResult<EverNoteUser> res = eum.AcivateUser(activae_id);

            if (res.Errors.Count > 0)
            {

                ErrorViewModel model = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    ıtems = res.Errors,
               
                };

            return View("Error", model);
            }
            OkViewModel okViewModel = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl="/Home/Login"
            };

            okViewModel.ıtems.Add("Hesabınnız Aktifleştirildi ");
            return RedirectToAction("Ok",okViewModel);
        }


        

         

        public ActionResult LogOut()
        {

            Session.Clear();

            return RedirectToAction("Index");
        }


        public ActionResult AccessDenied()
        {



            return View();
        }


        public ActionResult Exception()
        {

            return View();
        }

    }
}