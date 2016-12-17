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
    public class FilmsController : Controller
    {
        private FilmContext db = new FilmContext();

        // GET: Films
        public ActionResult Index()
        {
            return View(db.Films.ToList());
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        [HttpGet]
        // GET: Films/Create
        public ActionResult Create()
        {
            ViewBag.Actors = db.Actors.ToList();
            ViewBag.Genres = db.Genres.ToList();
            Film film = new Film();
            return View(film);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Director,Year")] Film film, int[] selectedActors, int[] selectedGenres)//, int[] selectedGenres, int[] selectedActors)
        {
            if (ModelState.IsValid)
            {
                db.Films.Add(film);
                if (selectedActors != null)
                {
                    foreach (var actor in db.Actors.Where(thisfilm => selectedActors.Contains(thisfilm.Id)))
                        film.Actors.Add(actor);
                }

                if (selectedGenres != null)
                {
                    foreach (var g in db.Genres.Where(fi => selectedGenres.Contains(fi.Id)))
                        film.Genres.Add(g);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Actors = db.Actors.ToList();
            ViewBag.Genres = db.Genres.ToList();
            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.Actors = db.Actors.ToList();
            ViewBag.Genres = db.Genres.ToList();
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Director,year")] Film film, int[] selectedActors, int[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                Film newFilm = db.Films.Find(film.Id);
                newFilm.Name = film.Name;
                newFilm.Description = film.Description;
                newFilm.Director = film.Director;
                newFilm.year = film.year;
                db.SaveChanges();

                newFilm.Actors.Clear();
                if (selectedActors != null)
                {
                    foreach (var actor in db.Actors.Where(thisfilm => selectedActors.Contains(thisfilm.Id)))
                        newFilm.Actors.Add(actor);
                }

                newFilm.Genres.Clear();
                if (selectedGenres != null)
                {
                    foreach (var genre in db.Genres.Where(fi => selectedGenres.Contains(fi.Id)))
                        newFilm.Genres.Add(genre);
                }

                db.Entry(newFilm).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
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
