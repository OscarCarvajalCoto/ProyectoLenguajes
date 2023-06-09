namespace ProyectoLenguajes.Models
{
    public class MovieOrSerieData
    {
        public Movie_Serie movie_serie { get; set; }
        public List<Actor> actors { get; set; }
        public string genres { get; set; }
        public Rating_Data rating_data { get; set; } = new Rating_Data();
        public string duration_h_m { get; set; }
        public class Rating_Data
        {
            public double average { get; set; }
            public int votes { get; set; } 
            public string percentage { get; set; }
        }
    }
}
