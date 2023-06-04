using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System;

namespace ProyectoLenguajes.Controllers
{
    public class Movie_SerieController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: Movie_SerieController
        public ActionResult Index()
        {
            return View(db.Movie_Series.ToList());
        }

        // GET: Movie_SerieController/Details/5
        public ActionResult Details(int ms_id)
        {
            return View(db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault());
        }

        // GET: Movie_SerieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie_SerieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie_Serie movie_serie)
        {
            try
            {
                db.Movie_Series.Add(movie_serie);
                db.SaveChanges();   
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie_SerieController/Edit/5
        public ActionResult Edit(int ms_id)
        {
            return View(db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault());
        }

        // POST: Movie_SerieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Movie_Serie movie_serie)
        {
            try
            {
                db.Update(movie_serie);
                db.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie_SerieController/Delete/5
        public ActionResult Delete(int ms_id)
        {
            return View(db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault());
        }

        // POST: Movie_SerieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movie_Serie movie_serie)
        {
            try
            {
                db.Movie_Series.Remove(movie_serie);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
