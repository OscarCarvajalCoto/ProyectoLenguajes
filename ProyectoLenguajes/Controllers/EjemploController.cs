using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoLenguajes.Controllers
{
    public class EjemploController : Controller
    {
        // GET: EjemploController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EjemploController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EjemploController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EjemploController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: EjemploController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EjemploController/Edit/5
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

        // GET: EjemploController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EjemploController/Delete/5
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
