using GCFinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GCFinalProject.App_Start
{
    public class MyDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
        protected override void Seed( ApplicationDbContext context)
             {
                 var UserManager = new UserManager<ApplicationUser>(new 

                                                UserStore<ApplicationUser>(context)); 

                 var RoleManager = new RoleManager<IdentityRole>(new 
                                          RoleStore<IdentityRole>(context));
      
                 string name = "Admin";
                 string password = "Test#1";
                 string email = "Admin@gmail.com";
 
     
                //Create Role Admin if it does not exist
                if (!RoleManager.RoleExists(name))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(name));
                }
     
                //Create User=Admin with password=123456
                var user = new ApplicationUser();
                user.UserName = name;
                user.Email = email;
                var adminresult = UserManager.Create(user, password);
     
                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, name);
                }
                base.Seed(context);
            }
        }
}