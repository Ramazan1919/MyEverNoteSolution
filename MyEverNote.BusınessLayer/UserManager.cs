using MyEverNote.BusınessLayer.Abstract;
using MyEverNote.BusınessLayer.Results;
using MyEverNote.Common.Helper;
using MyEverNote.DataAccessLayer.EntityFramework;
using MyEverNote.Entities;
using MyEverNote.Entities.Messages;
using MyEverNote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.BusınessLayer
{
   public   class UserManager : ManagerBase<EverNoteUser>
    {
       


        public BusınessLayerResult<EverNoteUser> RegisterUser(RegisterViewModels data )
        {
            EverNoteUser user =Find(x => x.Name == data.Name || x.Email == data.Email);
            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();
            if (user != null)
            {
                if (user.Name == data.Name)
                {
                    res.AddErrors(ErrorMessageCode.UserNameAlreadyExıst,"Kullanıcı Adı Kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddErrors(ErrorMessageCode.EmailAlreadyExıst, "E posta Kayıtlarda Var");
                }
            }
            else
            {
               int db_result =base.Insert(new EverNoteUser()
                {
                    UserName = data.Name,
                    Email = data.Email,
                    Password = data.Password,
                    ProfileImageFilename="",
                    ActıveGuıd = Guid.NewGuid(),
                    IsActıve = false,
                    IsAdmın = false
                });
               if (db_result > 0)
                {
                    res.Result=Find(  x=>x.Email==data.Email && x.UserName==data.Name);

                    string site = "http://localhost:54817";
                    string activateuri = $"{site}/Home/UserActivate/{res.Result.ActıveGuıd}";

                    string body = $" Merhaba {res.Result.UserName} Hesasabınız Aktifleştirmek için Lütfen <a href='{activateuri}'" +
                        $"target='_blank'>Tıklayınız</a>..";
                    MailHelper.SendMail(body,res.Result.Email,"Hesap Actifleştirme");

                }
            }
                  return res;
        }

        public BusınessLayerResult<EverNoteUser> GetUserById(int id)
        {


            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();

            res.Result = Find(x => x.Id==id);



            if (res.Result == null)
            {

                res.AddErrors(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");
            }

            return res;
        }

        public BusınessLayerResult<EverNoteUser> LoginUser(LoginViewModels data)
        {

               BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();
               res.Result=Find(x => x.UserName == data.UserName || x.Email == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActıve)
                {
                    res.AddErrors(ErrorMessageCode.UserIsNotActıve, "Kullanıcı Aktif değil");
                    res.AddErrors(ErrorMessageCode.ActıvateEposta, "E-Postanızı Kontrol edin");

                }
               
            }
            else
            {
                res.AddErrors(ErrorMessageCode.UserNameorPassWrong, "Kullanıcı Adı Veya Şifre Uyuşmuyor");
            }
            return res;

        }



        public BusınessLayerResult<EverNoteUser> AcivateUser (Guid activateId)
        {

            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();
            res.Result = Find(x => x.ActıveGuıd == activateId);


            if (res != null)
            {

                if (res.Result.IsActıve)
                {
                    res.AddErrors(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiş");

                    return res;
                }

                res.Result.IsActıve = true;
                Update(res.Result);
            }
            else
            {

                res.AddErrors(ErrorMessageCode.ActıvateIdDoesNotExıst, "Böyle bir kullanıcı yok");
            }

            return res;
        }


        public BusınessLayerResult<EverNoteUser> UpdateUser(EverNoteUser data)
        {
            // EverNoteUser db_user = repo_user.Find(x => x.Id != data.Id && (x.UserName == data.UserName || x.Email == data.Email));
            EverNoteUser db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();


            if(db_user ==null && db_user.Id == data.Id)
            {

                if (db_user.UserName == data.UserName)
                {

                    res.AddErrors(ErrorMessageCode.UserNameAlreadyExıst, "Kullanıcı adı kayıtlı");

                }

                //if (db_user.Email == data.Email)
                //{

                //    res.AddErrors(ErrorMessageCode.EmailAlreadyExıst, "Email  kayıtlı");

                //}

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Name = data.Name;
            res.Result.Password = data.Password;
            res.Result.Surname = data.Surname;
            res.Result.Email = data.Email;
            res.Result.UserName = data.UserName;



            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {

                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {

                res.AddErrors(ErrorMessageCode.ProfileIsNotUpdate, "Profil Güncellenmedi");
            }


            return res;


        }

        public BusınessLayerResult<EverNoteUser> DeleteUserById(int id)
        {
            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();
            EverNoteUser DeleteUser = Find(x => x.Id == id);
            if (DeleteUser != null)
            {

                if (Delete(DeleteUser) == 0)
                {
                    res.AddErrors(ErrorMessageCode.UserNotDelete, "Kullanıcı Silinmedi");

                    return res;

                }

            }
            else
            {

                res.AddErrors(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");

               
            }

            return res;
        }



        public new BusınessLayerResult<EverNoteUser> Insert (EverNoteUser data)
        {
            //method hiding ...


            EverNoteUser user = Find(x => x.Name == data.Name || x.Email == data.Email);
            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();

            res.Result = data;
            if (user != null)
            {
                if (user.Name == data.Name)
                {
                    res.AddErrors(ErrorMessageCode.UserNameAlreadyExıst, "Kullanıcı Adı Kayıtlı");
                }
                if (user.Email == data.Email)
                {
                    res.AddErrors(ErrorMessageCode.EmailAlreadyExıst, "E posta Kayıtlarda Var");
                }
            }
            else
            {

                res.Result.ProfileImageFilename = "";
                res.Result.ActıveGuıd = Guid.NewGuid();

                if (base.Insert(res.Result) == 0)
                {
                    res.AddErrors(ErrorMessageCode.USerNotAdded, "Kullanıcı Eklenemedi");
                } 
               
            }
            return res;


        }


        public new BusınessLayerResult<EverNoteUser> Update(EverNoteUser data)
        {

            EverNoteUser db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusınessLayerResult<EverNoteUser> res = new BusınessLayerResult<EverNoteUser>();


            res.Result = data;
            if (db_user == null && db_user.Id == data.Id)
            {

                if (db_user.UserName == data.UserName)
                {

                    res.AddErrors(ErrorMessageCode.UserNameAlreadyExıst, "Kullanıcı adı kayıtlı");

                }

                if (db_user.Email == data.Email)
                {

                    res.AddErrors(ErrorMessageCode.EmailAlreadyExıst, "Email  kayıtlı");

                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Name = data.Name;
            res.Result.Password = data.Password;
            res.Result.Surname = data.Surname;
            res.Result.Email = data.Email;
            res.Result.UserName = data.UserName;
            res.Result.IsActıve = data.IsActıve;
            res.Result.IsAdmın = data.IsAdmın;



            if (base.Update(res.Result) == 0)
            {

                res.AddErrors(ErrorMessageCode.UserIsNotUpdate, "Kullanıcı Güncellenmedi");
            }


            return res;


        }
    }
}
