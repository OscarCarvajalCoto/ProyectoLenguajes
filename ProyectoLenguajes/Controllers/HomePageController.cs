using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using System.Data;

namespace ProyectoLenguajes.Controllers
{
    public class HomePageController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        // GET: HomePageController
        public async Task<ActionResult> MovieOrSerieDetails(int ms_id)
        {
            var movie_serie_data = new MovieOrSerieData();
            movie_serie_data.movie_serie = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault();
            movie_serie_data.actors = db.Actors.FromSqlRaw(@"exec Get_Movie_Serie_Actor @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();
            movie_serie_data.genres = db.Genres.FromSqlRaw(@"exec Get_Movie_Serie_Genre @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();

            var average = new SqlParameter("@average", SqlDbType.Float) { Direction = ParameterDirection.Output };
            var votes = new SqlParameter("@votes", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var percentage = new SqlParameter("@percentage", SqlDbType.VarChar, 4) { Direction = ParameterDirection.Output };
            await db.Database.ExecuteSqlRawAsync(@"Get_Rating_Data @ms_id, @average OUT, @votes OUT, @percentage OUT", new SqlParameter("@ms_id", ms_id), average, votes, percentage);

            if (average.Value != DBNull.Value)
                movie_serie_data.rating_data.average = (double)average.Value;
            else
                movie_serie_data.rating_data.average = 0;
            if (votes.Value != DBNull.Value)
                movie_serie_data.rating_data.votes = (int)votes.Value;
            else
                movie_serie_data.rating_data.votes = 0;
            if (percentage.Value != DBNull.Value)
                movie_serie_data.rating_data.percentage = (string)percentage.Value;
            else
                movie_serie_data.rating_data.percentage = "100%";

            return View(movie_serie_data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rate(Rating rating)
        {
            try
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("MovieOrSerieDetails", new { rating.ms_id });
            }
            catch
            {
                ViewBag.ms_id = rating.ms_id;
                ViewBag.Message = new Message() { Text = "The rating has not been added successfully", Tipo = Alerta.danger.ToString() };
                return View();
            }
        }

        public async Task<string> GetNewRating(int rating, string app_user, int ms_id)
        {
            var ratingBD = new Rating() {rating1 = rating, app_user = app_user, ms_id = ms_id };
            db.Ratings.Add(ratingBD);
            db.SaveChanges();

            var newRating = new MovieOrSerieData.Rating_Data();

            var average = new SqlParameter("@average", SqlDbType.Float) { Direction = ParameterDirection.Output };
            var votes = new SqlParameter("@votes", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var percentage = new SqlParameter("@percentage", SqlDbType.VarChar, 4) { Direction = ParameterDirection.Output };
            await db.Database.ExecuteSqlRawAsync(@"Get_Rating_Data @ms_id, @average OUT, @votes OUT, @percentage OUT", new SqlParameter("@ms_id", ms_id), average, votes, percentage);

            if (average.Value != DBNull.Value)
                newRating.average = (double)average.Value;
            else
                newRating.average = 0;
            if (votes.Value != DBNull.Value)
                newRating.votes = (int)votes.Value;
            else
                newRating.votes = 0;
            if (percentage.Value != DBNull.Value)
                newRating.percentage = (string)percentage.Value;
            else
                newRating.percentage = "100%";

            return JsonConvert.SerializeObject(newRating);
        }
    }
}
