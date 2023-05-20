using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class MoviesAndSeries
{
    public int id { get; set; }

    public string title { get; set; } = null!;

    public string synopsis { get; set; } = null!;

    public int duration { get; set; }

    public string classificacion { get; set; } = null!;

    public string director { get; set; } = null!;

    public int genre_id { get; set; }

    public int num_seasons { get; set; }

    public int num_episodes { get; set; }

    public int episode_duration { get; set; }

    public string movie_cover { get; set; } = null!;

    public DateTime year_of_release { get; set; }
}
