using GCFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCFinalProject.DAL
{
    public class EventSiteInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<EventSiteContext>
    {
        protected override void Seed(EventSiteContext context){
            var categorys = new List<Category>
            {
                new Category { CategoryID=1,CategoryName="Food and Drinks"},
                new Category { CategoryID=2,CategoryName="Music"},
                new Category { CategoryID=3,CategoryName="Arts"},
                new Category { CategoryID=4,CategoryName="Sports"}
            };
        }
    }
}