using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class Movie_Serie
{
    public int ms_id { get; set; }

    public string tittle { get; set; } = null!;

    public string synopsis { get; set; } = null!;

    public int release_year { get; set; }

    public string classificationMS { get; set; } = null!;

    public string director { get; set; } = null!;

    public string cover { get; set; } = null!;

    public DateTime date_added { get; set; }

    public double? duration { get; set; }

    public string? ms_type { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
