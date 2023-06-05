using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class RatingController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: RatingController
        public ActionResult Index(int ms_id)
        {
            ViewBag.ms_id = ms_id;
            ViewBag.ms_tittle = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault().tittle;
            return View(db.Ratings.ToList().Where(x => x.ms_id == ms_id));
        }

        // GET: RatingController/Details/5
        public ActionResult Details(int rating_id)
        {
            return View(db.Ratings.Where(x => x.rating_id == rating_id).FirstOrDefault());
        }

        // GET: RatingController/Create
        public ActionResult Create(int ms_id)
        {
            ViewBag.ms_id = ms_id;
            return View();
        }

        // POST: RatingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rating rating)
        {
            try
            {
                db.Ratings.Add(rating); 
                db.SaveChanges();
                return RedirectToAction("Index",new { rating.ms_id});
            }
            catch
            {
                ViewBag.ms_id = rating.ms_id;
                ViewBag.Message = new Message() { Text = "The rating has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: RatingController/Edit/5
        public ActionResult Edit(int rating_id)
        {
            return View(db.Ratings.Where(x => x.rating_id == rating_id).FirstOrDefault());
        }

        // POST: RatingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Rating rating)
        {
            try
            {
                db.Ratings.Update(rating);
                db.SaveChanges();
                return RedirectToAction("Index", new { rating.ms_id });
            }
            catch
            {
                ViewBag.ms_id = rating.ms_id;
                ViewBag.Message = new Message() { Text = "The rating has not been updated successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: RatingController/Delete/5
        public ActionResult Delete(int rating_id)
        {
            return View(db.Ratings.Where(x => x.rating_id == rating_id).FirstOrDefault());
        }

        // POST: RatingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Rating rating)
        {
            try
            {
                db.Ratings.Remove(rating);
                db.SaveChanges();
                return RedirectToAction("Index", new {rating.ms_id});
            }
            catch
            {
                ViewBag.ms_id = rating.ms_id;
                ViewBag.Message = new Message() { Text = "The rating has not been deleted successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }
    }
}
