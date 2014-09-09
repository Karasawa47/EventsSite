using GCFinalProject.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private EventSiteContext db = new EventSiteContext();

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

        public ActionResult CalendarPage()
        {

            ViewBag.Date = DateTime.Now;
            var calendarDate = DateTime.Now;
            var firstDate = new DateTime(calendarDate.Year, calendarDate.Month, 1);
            var lastDate = new DateTime(calendarDate.Year, calendarDate.Month, DateTime.DaysInMonth(calendarDate.Year, calendarDate.Month));
            var events = from e in db.Events
                         where (e.StartDate >= DbFunctions.TruncateTime(firstDate) && e.StartDate <= DbFunctions.TruncateTime(lastDate))
                     orderby e.StartDate
                     select e;
            return View(events);
        }
    }
}