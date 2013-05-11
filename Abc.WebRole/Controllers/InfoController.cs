using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.WebRole.Models;
using Abc.WebRole.InfoRequestDb__Abc.WebRole;

namespace Abc.WebRole.Controllers
{
    public class InfoController : Controller
    {
        private Models_ db = new Models_();

        //
        // GET: /Info/

        public ActionResult Index()
        {
            return View(db.InfoRequests.ToList());
        }

        //
        // GET: /Info/Details/5

        public ActionResult Details(long id = 0)
        {
            InfoRequest inforequest = db.InfoRequests.Find(id);
            if (inforequest == null)
            {
                return HttpNotFound();
            }
            return View(inforequest);
        }

        //
        // GET: /Info/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Info/Create

        [HttpPost]
        public ActionResult Create(InfoRequest inforequest)
        {
            if (ModelState.IsValid)
            {
                db.InfoRequests.Add(inforequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inforequest);
        }

        //
        // GET: /Info/Edit/5

        public ActionResult Edit(long id = 0)
        {
            InfoRequest inforequest = db.InfoRequests.Find(id);
            if (inforequest == null)
            {
                return HttpNotFound();
            }
            return View(inforequest);
        }

        //
        // POST: /Info/Edit/5

        [HttpPost]
        public ActionResult Edit(InfoRequest inforequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inforequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inforequest);
        }

        //
        // GET: /Info/Delete/5

        public ActionResult Delete(long id = 0)
        {
            InfoRequest inforequest = db.InfoRequests.Find(id);
            if (inforequest == null)
            {
                return HttpNotFound();
            }
            return View(inforequest);
        }

        //
        // POST: /Info/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            InfoRequest inforequest = db.InfoRequests.Find(id);
            db.InfoRequests.Remove(inforequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}