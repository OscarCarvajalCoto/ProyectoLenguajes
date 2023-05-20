using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class Genre
{
    public int genre_id { get; set; }

    public int movie_series_id { get; set; }

    public string genre_name { get; set; } = null!;

    public virtual ICollection<MoviesAndSeriesGenre> MoviesAndSeriesGenres { get; set; } = new List<MoviesAndSeriesGenre>();
}
