using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class Movie_Serie_GenreController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: Movie_Serie_GenreController
        public ActionResult Index(int ms_id)
        {
            var movie_serie = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault();
            var genres_e = db.Genres.FromSqlRaw(@"exec Get_Movie_Serie_Genre @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();
            var genres_i = db.Genres.FromSqlRaw(@"exec Not_Get_Movie_Serie_Genre @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();
            ViewBag.IntermediateMSG = new IntermediateMSG() { movie_serie = movie_serie, genres_e = genres_e, genres_i = genres_i };
            return View();
        }

        // GET: Movie_Serie_GenreController/Create
        public ActionResult Create(int ms_id)
        {
            return View();
        }

        // POST: Movie_Serie_GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ms_id, Movie_Serie_Genre msg)
        {
            try
            {
                db.Movie_Serie_Genres.Add(msg);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg.ms_id });
            }
            catch
            {
                ViewBag.ms_id = msg.ms_id;
                ViewBag.Message = new Message() { Text = "The actor has not been added to the movie successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: Movie_Serie_GenreController/Delete/5
        public ActionResult Delete(int ms_id, int genre_id)
        {
            return View();
        }

        // POST: Movie_Serie_GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ms_id, Movie_Serie_Genre msg)
        {
            try
            {
                db.Movie_Serie_Genres.Remove(msg);
                db.SaveChanges();
                return RedirectToAction("Index", new { msg.ms_id });
            }
            catch
            {
                ViewBag.ms_id = msg.ms_id;
                ViewBag.Message = new Message() { Text = "The actor has not been removed to the movie successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }
    }
}
