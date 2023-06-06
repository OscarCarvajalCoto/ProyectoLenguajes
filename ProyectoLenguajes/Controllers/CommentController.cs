using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: CommentController
        public ActionResult Index(int ms_id)
        {
            ViewBag.ms_id = ms_id;
            ViewBag.ms_tittle = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault().tittle;
            return View(db.Comments.ToList().Where(x => x.ms_id == ms_id));
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int comment_id)
        {
            return View(db.Comments.Where(x=>x.comment_id == comment_id).FirstOrDefault());
        }

        // GET: CommentController/Create
        public ActionResult Create(int ms_id)
        {
            ViewBag.ms_id = ms_id;
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            try
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", new {comment.ms_id});
            }
            catch
            {
                ViewBag.ms_id = comment.ms_id;
                ViewBag.Message = new Message() { Text = "The comment has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int comment_id)
        {
            return View(db.Comments.Where(x => x.comment_id == comment_id).FirstOrDefault());
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                db.Comments.Update(comment);
                db.SaveChanges();
                return RedirectToAction("Index", new {comment.ms_id });
            }
            catch
            {
                ViewBag.ms_id = comment.ms_id;
                ViewBag.Message = new Message() { Text = "The comment has not been updated successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int comment_id)
        {
            return View(db.Comments.Where(x => x.comment_id == comment_id).FirstOrDefault());
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            try
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Index", new {comment.ms_id });
            }
            catch
            {
                ViewBag.ms_id = comment.ms_id;
                ViewBag.Message = new Message() { Text = "The comment has not been deleted successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }
    }
}
