using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class ActorController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: ActorController
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        // GET: ActorController/Details/5
        public ActionResult Details(int actor_id)
        {
            return View(db.Actors.Where(x => x.actor_id == actor_id).FirstOrDefault());
        }

        // GET: ActorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor)
        {
            try
            {
                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = new Message() { Text = "The actor has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: ActorController/Edit/5
        public ActionResult Edit(int actor_id)
        {
            return View(db.Actors.Where(x => x.actor_id == actor_id).FirstOrDefault());
        }

        // POST: ActorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Actor actor)
        {
            try
            {
                db.Actors.Update(actor);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = new Message() { Text = "The actor has not been updated successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: ActorController/Delete/5
        public ActionResult Delete(int actor_id)
        {
            return View(db.Actors.Where(x => x.actor_id == actor_id).FirstOrDefault());
        }

        // POST: ActorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Actor actor)
        {
            try
            {
                db.Actors.Remove(actor);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = new Message() { Text = "The actor has not been deleted successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

    }
}
