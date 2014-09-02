using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCFinalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Downtown Detroit is a year-round destination for sports and entertainment. The Tigers, Lions, and Red Wings all call downtown Detroit home. The city also boasts top-notch venues for live entertainment, including the Fox Theatre, Detroit Opera House, The Fillmore Detroit, Gem Theatre, and Music Hall. Parts of the city's riverfront have been revitalized in recent years, adding the Detroit RiverWalk and William G. Milliken State Park and Harbor. During the summer, Chene Park and Hart Plaza welcome visitors to the riverfront area for concerts, festivals and other events. Detroit Events is striving to provide  Detroit visitors with the most up to date event schedule.";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Detroit Events";

            return View();
        }
    }
}