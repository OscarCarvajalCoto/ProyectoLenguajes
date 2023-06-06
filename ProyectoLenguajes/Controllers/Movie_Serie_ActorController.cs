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
    public class Movie_Serie_ActorController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: Movie_Serie_ActorController
        public ActionResult Index(int ms_id)
        {
            var movie_serie = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault();
            var actors_e = db.Actors.FromSqlRaw(@"exec Get_Movie_Serie_Actor @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();
            var actors_i = db.Actors.FromSqlRaw(@"exec Not_Get_Movie_Serie_Actor @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();
            ViewBag.IntermediateMSA = new IntermediateMSA() { movie_serie = movie_serie, actors_e = actors_e, actors_i = actors_i };
            return View();
        }

        // GET: Movie_Serie_ActorController/Create
        public ActionResult Create(int ms_id)
        {
            return View();
        }

        // POST: Movie_Serie_ActorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ms_id, Movie_Serie_Actor msa)
        {
            try
            {
                db.Movie_Serie_Actors.Add(msa);
                db.SaveChanges();
                return RedirectToAction("Index", new { msa.ms_id });
            }
            catch
            {
                ViewBag.ms_id = msa.ms_id;
                ViewBag.Message = new Message() { Text = "The actor has not been added to the movie successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: Movie_Serie_ActorController/Delete/5
        public ActionResult Delete(int ms_id, int actor_id)
        {
            return View();
        }

        // POST: Movie_Serie_ActorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ms_id, Movie_Serie_Actor msa)
        {
            try
            {
                db.Movie_Serie_Actors.Remove(msa);
                db.SaveChanges();
                return RedirectToAction("Index", new {msa.ms_id});
            }
            catch
            {
                ViewBag.ms_id = msa.ms_id;
                ViewBag.Message = new Message() { Text = "The actor has not been removed to the movie successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }
    }
}
