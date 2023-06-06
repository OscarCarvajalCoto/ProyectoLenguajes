using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class EpisodeController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: EpisodeController
        public ActionResult Index(int ms_id)
        {
            ViewBag.ms_id = ms_id;  
            ViewBag.ms_tittle = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault().tittle;
            return View(db.Episodes.ToList().Where(x => x.ms_id == ms_id));
        }

        // GET: EpisodeController/Details/5
        public ActionResult Details(int ms_id, int season_id, int episode_id)
        {
            return View(db.Episodes.Where(x => x.ms_id == ms_id && x.season_id == season_id && x.episode_id == episode_id).FirstOrDefault());
        }

        // GET: EpisodeController/Create
        public ActionResult Create(int ms_id)
        {
            ViewBag.ms_id = ms_id;
            return View();
        }

        // POST: EpisodeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Episode episode)
        {
            try
            {
                db.Episodes.Add(episode);
                db.SaveChanges();
                return RedirectToAction("Index", new { episode.ms_id });
            }
            catch
            {
                ViewBag.ms_id = episode.ms_id;
                ViewBag.Message = new Message() { Text = "The episode has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: EpisodeController/Edit/5
        public ActionResult Edit(int ms_id, int season_id, int episode_id)
        {
            return View(db.Episodes.Where(x => x.ms_id == ms_id && x.season_id == season_id && x.episode_id == episode_id).FirstOrDefault());
        }

        // POST: EpisodeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Episode episode)
        {
            try
            {
                db.Update(episode);
                db.SaveChanges();
                return RedirectToAction("Index", new { episode.ms_id });
            }
            catch
            {
                ViewBag.ms_id = episode.ms_id;
                ViewBag.Message = new Message() { Text = "The episode has not been updated successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: EpisodeController/Delete/5
        public ActionResult Delete(int ms_id, int season_id, int episode_id)
        {
            return View(db.Episodes.Where(x => x.ms_id == ms_id && x.season_id == season_id && x.episode_id == episode_id).FirstOrDefault());
        }

        // POST: EpisodeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Episode episode)
        {
            try
            {
                db.Remove(episode);
                db.SaveChanges();
                return RedirectToAction("Index", new { episode.ms_id });
            }
            catch
            {
                ViewBag.ms_id = episode.ms_id;
                ViewBag.Message = new Message() { Text = "The episode has not been deleted successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }
    }
}
