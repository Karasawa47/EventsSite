namespace GCFinalProject.Migrations
{
    using GCFinalProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GCFinalProject.DAL.EventSiteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "GCFinalProject.DAL.EventSiteContext";
        }

        protected override void Seed(GCFinalProject.DAL.EventSiteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var categorys = new List<Category>
            {
                new Category { CategoryID=1,CategoryName="Food and Drinks"},
                new Category { CategoryID=2,CategoryName="Music"},
                new Category { CategoryID=3,CategoryName="Art"},
                new Category { CategoryID=4,CategoryName="Sport"},
                new Category { CategoryID=5,CategoryName="Festival"},
                new Category { CategoryID=6,CategoryName="Conference"},
                new Category { CategoryID=7,CategoryName="Convention"},
                new Category { CategoryID=8,CategoryName="Parade"}

            };
            categorys.ForEach(c => context.Categorys.AddOrUpdate(p => p.CategoryID, c));
            context.SaveChanges();
            var context2 = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new

                                                UserStore<ApplicationUser>(context2));

            var RoleManager = new RoleManager<IdentityRole>(new
                                     RoleStore<IdentityRole>(context2));

            string name = "Admin@gmail.com";
            string password = "Test#1";
            string email = "Admin@gmail.com";
            string roleName = "Admin";


            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(roleName))
            {
                var roleresult = RoleManager.Create(new IdentityRole(roleName));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            user.Email = email;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, roleName);
            }
            ;
            base.Seed(context);
        }
        
    }
}
