using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema.Models;

namespace Cinema.Controllers
{
    public class MCinemasController : Controller
    {
        private FilmContext db = new FilmContext();

        // GET: MCinemas
        public ActionResult Index()
        {
            return View(db.Cinemas.ToList());
        }

        // GET: MCinemas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCinema mCinema = db.Cinemas.Find(id);
            if (mCinema == null)
            {
                return HttpNotFound();
            }
            List<Hall> halls = new List<Hall>();
            foreach (var hall in db.Halls)
            {
                if (hall.cinema == mCinema)
                    halls.Add(hall);
            }
            ViewBag.Halls = halls;
            return View(mCinema);
        }

        

        // GET: MCinemas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MCinemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Adress")] MCinema mCinema)
        {
            if (ModelState.IsValid)
            {
                db.Cinemas.Add(mCinema);
                db.SaveChanges();
                mCinema.Halls = null;
                Hall hall = new Hall();
                hall.Number = 1;
                hall.NumberSeats = 10;
                hall.cinema = mCinema;
                db.Halls.Add(hall);
                db.SaveChanges();
                mCinema.Halls.Add(hall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mCinema);
        }

        public ActionResult AddHall( int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCinema cinema = db.Cinemas.Find(id);
            Hall hall = new Hall();
            hall.cinema = cinema;
            int max = 0;
            foreach(Hall hal in db.Halls)
            {
                if (hal.cinema == cinema && hal.Number > max)
                    max = hal.Number;
            }
            hall.Number = max + 1;
            hall.NumberSeats = 10;
            db.Halls.Add(hall);
            db.SaveChanges();
            return RedirectToAction("Edit", new {id = id });
        }

        // GET: MCinemas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCinema mCinema = db.Cinemas.Find(id);
            List<Hall> halls = new List<Hall>();
            foreach (var hall in db.Halls)
            {
                if(hall.cinema == mCinema)
                    halls.Add(hall);
            }
            if (mCinema == null)
            {
                return HttpNotFound();
            }
            ViewBag.Halls = halls;
            return View(mCinema);
        }

        // POST: MCinemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Adress")] MCinema mCinema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCinema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mCinema);
        }

        // GET: MCinemas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCinema mCinema = db.Cinemas.Find(id);
            if (mCinema == null)
            {
                return HttpNotFound();
            }
            return View(mCinema);
        }

        // POST: MCinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MCinema mCinema = db.Cinemas.Find(id);
            db.Cinemas.Remove(mCinema);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult HallDetails(int? id)
        {
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            ViewBag.cinema = db.Cinemas.Find(hall.CinemaId).Name;
            return View(hall);
        }

        
        public ActionResult HallEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = db.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            ViewBag.cinema = db.Cinemas.Find(hall.CinemaId).Name;
            return View(hall);
        }

        [HttpPost]
        public ActionResult HallEdit( Hall hall)
        {
            Hall nhall = db.Halls.Find(hall.Id);
            nhall.Number = hall.Number;
            nhall.NumberSeats = hall.NumberSeats;
            nhall.CinemaId = hall.CinemaId;
            nhall.cinema = db.Cinemas.Find(nhall.CinemaId);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = hall.CinemaId });


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
