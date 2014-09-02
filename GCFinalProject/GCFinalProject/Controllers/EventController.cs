using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GCFinalProject.DAL;
using GCFinalProject.Models;
using System.Diagnostics;
using System.Data.Entity.Core.Objects;

namespace GCFinalProject.Controllers
{
    public class EventController : Controller
    {
        private EventSiteContext db = new EventSiteContext();

        // GET: Event
        public ActionResult Index(DateTime? dateS,DateTime? dateE,string currentFilter,string searchString,int? categoryID)
        {
            if (searchString != null)
            {
                //page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var events = from e in db.Events
                            select e;
            if (!User.IsInRole("Admin"))
            {
                events = from e in db.Events
                         where e.IsActive == true
                         orderby e.StartDate
                         select e;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                events = from e in events
                         //where e.EventName.ToUpper() == (searchString).ToUpper()
                         where e.EventName.ToUpper().Contains(searchString.ToUpper())
                         orderby e.StartDate
                         select e;
            }
            if (categoryID != null && categoryID > 0)
            {
                events = from e in events
                         join c in db.Categorys
                         on e.CategoryID equals c.CategoryID
                         where c.CategoryID == (int)categoryID
                         orderby e.StartDate
                         select e;
            }
            if(dateS!=null && dateE !=null){
                events = from e in events
                         where (e.StartDate >= DbFunctions.TruncateTime(dateS) && e.StartDate <= DbFunctions.TruncateTime(dateE))
                         orderby e.StartDate
                         select e;
            }
            if (dateS != null && dateE == null)
            {
                events = from e in events
                         where (e.StartDate >= DbFunctions.TruncateTime(dateS) && e.StartDate <= DbFunctions.TruncateTime((dateS)))
                         orderby e.StartDate
                         select e;
            }
            PopulateCategoryDropDown();
            return View(events);
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryName = (from c in db.Categorys
                                   where c.CategoryID == @event.CategoryID
                                   select c.CategoryName).Single();
            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            PopulateCategoryDropDown();
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventDesc,EventName,StartDate,EndDate,StartTime,EndTime,IsActive,ContactInfo,LinkUrl,ImageUrl,CategoryID,Location,ShortSummary")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            PopulateCategoryDropDown();
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventDesc,EventName,StartDate,EndDate,StartTime,EndTime,IsActive,ContactInfo,LinkUrl,ImageUrl,CategoryID,Location,ShortSummary")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void PopulateCategoryDropDown()
        {
            var categoryQuery = (from c in db.Categorys
                                   orderby c.CategoryName
                                   select  c);
            
            ViewBag.SelectListCategory = new SelectList(categoryQuery, "CategoryID","CategoryName");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
