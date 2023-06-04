using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;

namespace ProyectoLenguajes.Controllers
{
    public class EpisodeController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: EpisodeController
        public ActionResult Index(int ms_id)
        {
            GeneralData.current_ms_id = ms_id;
            GeneralData.current_ms_title = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault().tittle;
            ViewBag.ms_tittle = GeneralData.current_ms_title;
            return View(db.Episodes.ToList().Where(x => x.ms_id == ms_id));
        }

        // GET: EpisodeController/Details/5
        public ActionResult Details(int ms_id, int season_id, int episode_id)
        {
            return View(db.Episodes.Where(x => x.ms_id == ms_id && x.season_id == season_id && x.episode_id == episode_id).FirstOrDefault());
        }

        // GET: EpisodeController/Create
        public ActionResult Create()
        {
            ViewBag.ms_id = GeneralData.current_ms_id;
            ViewBag.ms_tittle = GeneralData.current_ms_title;
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
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: EpisodeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EpisodeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EpisodeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EpisodeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
