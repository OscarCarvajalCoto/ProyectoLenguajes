using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenreController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: GenreController
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int genre_id)
        {
            return View(db.Genres.Where(x => x.genre_id == genre_id).FirstOrDefault());
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            try
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = new Message() { Text = "The actor has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int genre_id)
        {
            return View(db.Genres.Where(x => x.genre_id == genre_id).FirstOrDefault());
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int genre_id, Genre genre)
        {
            try
            {
                db.Genres.Update(genre);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = new Message() { Text = "The actor has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int genre_id)
        {
            return View(db.Genres.Where(x => x.genre_id == genre_id).FirstOrDefault());
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int genre_id, Genre genre)
        {
            try
            {
                db.Genres.Remove(genre);    
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = new Message() { Text = "The actor has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }
    }
}
