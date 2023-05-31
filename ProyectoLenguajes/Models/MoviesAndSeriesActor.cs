namespace ProyectoLenguajes.Models;

public partial class MoviesAndSeriesActor
{
    public int movies_series_id { get; set; }

    public int actor_id { get; set; }

    public virtual Actor actor { get; set; } = null!;

    public virtual MoviesAndSeriesGenre movies_series { get; set; } = null!;
}
