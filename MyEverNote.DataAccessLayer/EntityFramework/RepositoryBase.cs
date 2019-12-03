using MyEverNote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {


        protected static  DatabaseContext context;
        private static object _lockSync = new object();

        public RepositoryBase()
        {

            context = CreateContext();
        }

        protected static DatabaseContext CreateContext()
        {

            if (context == null)
            {
                lock (_lockSync)
                {
                    if (context == null)
                    {

                        context = new DatabaseContext();
                    }

                }
               
                 
            }

            return context;
        }


    }
}
