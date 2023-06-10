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
        public ActionResult Index()
        {
            return View();
        }
        // GET: HomePageController
        public async Task<ActionResult> MovieOrSerieDetails(int ms_id)
        {
            var movie_serie_data = new MovieOrSerieData();
            movie_serie_data.movie_serie = db.Movie_Series.Where(x => x.ms_id == ms_id).FirstOrDefault();
            movie_serie_data.actors = db.Actors.FromSqlRaw(@"exec Get_Movie_Serie_Actor @ms_id", new SqlParameter("@ms_id", ms_id)).ToList();
            
            var genres = new SqlParameter("@genres", SqlDbType.VarChar, int.MaxValue) { Direction = ParameterDirection.Output };
            await db.Database.ExecuteSqlRawAsync(@"Get_String_Genres @ms_id, @genres OUT", new SqlParameter("@ms_id", ms_id), genres);

            movie_serie_data.genres = (string) genres.Value;

            if (movie_serie_data.movie_serie.ms_type == "movie")
            {
                int h = (int)movie_serie_data.movie_serie.duration / 60;
                movie_serie_data.duration_h_m = h + "h " + (movie_serie_data.movie_serie.duration - h * 60) + "m";
            }
            else movie_serie_data.duration_h_m = "";

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

        public async Task<string> GetNewRating(int rating, string app_user, int ms_id)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@rating", rating));
            parameter.Add(new SqlParameter("@app_user", app_user));
            parameter.Add(new SqlParameter("@ms_id", ms_id));

            await db.Database.ExecuteSqlRawAsync(@"exec Create_Rating @rating, @app_user, @ms_id", parameter.ToArray());
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
