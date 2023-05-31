namespace ProyectoLenguajes.Models;

public partial class MoviesAndSeriesGenre
{
    public int movies_series_id { get; set; }

    public int genre_id { get; set; }

    public virtual Genre genre { get; set; } = null!;
}
