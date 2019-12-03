namespace MyEverNote.DataAccessLayer.Migrations
{
    using MyEverNote.DataAccessLayer.EntityFramework;
    using MyEverNote.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Seed2(context);
        }

        public void Seed2(DatabaseContext context)
        {

            EverNoteUser admin = new EverNoteUser()
            {
                Name = "Ramazan",
                ActýveGuýd = Guid.NewGuid(),
                IsActýve = true,
                Email = "ramazansokuoglu@gmail.com",
                Surname = "Sokuoðlu",
                IsAdmýn = true,
                ProfileImageFilename = "boy.png",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "Ramo",
                UserName = "Ramazan",
                Password = "12345",

            };

            context.EverNoteUsers.Add(admin);
            context.SaveChanges();


        }
    }
}