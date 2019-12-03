using MyEverNote.Common;
using MyEverNote.Core.DataAccess;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
   public    class Repository<T> :RepositoryBase,IRepository<T> where T  : class
    {
      
        private DbSet<T> _objectSet;

        public Repository()
        {

           
            _objectSet = context.Set<T>();

        }

        public List<T> List()
        {

            return _objectSet.ToList();
        }

        public int Insert(T obj)
        {

            _objectSet.Add(obj);

            if(obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime on = DateTime.Now;
                o.CreatedOn = on;
                o.ModifiedOn = on;
                o.ModifiedUsername =App.common.GetUsername();
                

                    
            }

            return Save();

               
        }

       
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public int Update(T  obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime on = DateTime.Now;
                
                o.ModifiedOn = on;
                o.ModifiedUsername = App.common.GetUsername();


            }
            return Save();
        }

        public int Delete(T obj)
        {

            _objectSet.Remove(obj);
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;
            //    DateTime on = DateTime.Now;

            //    o.ModifiedOn = on;
            //    o.ModifiedUsername = "system";


            //}
            return Save();
        }


        public int Save()
        {

            return context.SaveChanges();
        }


        public List<T> List(Expression<Func<T,bool>>where)
        {

            return _objectSet.Where(where).ToList();
        }


        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);


        }





    }
}
