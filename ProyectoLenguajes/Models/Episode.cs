using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class Episode
{
    public int ms_id { get; set; }

    public int season_id { get; set; }

    public int episode_id { get; set; }

    public string tittle { get; set; } = null!;

    public double duration { get; set; }
}
