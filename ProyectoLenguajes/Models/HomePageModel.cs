namespace ProyectoLenguajes.Models
{
    public class HomePageModel
    {

        public List<Movie_Serie> last_ten { get; set; }

        public string genre1 { get; set; }
        public List<Movie_Serie> carrousel_1 { get; set; }
        public string genre2 { get; set; }

        public List<Movie_Serie> carrousel_2 { get; set; }
        public string genre3 { get; set; }

        public List<Movie_Serie> carrousel_3 { get; set; }
    }
}
