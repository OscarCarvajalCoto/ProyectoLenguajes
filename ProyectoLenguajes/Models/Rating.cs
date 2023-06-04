using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class Rating
{
    public int rating_id { get; set; }

    public int rating1 { get; set; }

    public string app_user_id { get; set; } = null!;

    public int ms_id { get; set; }

    public virtual Movie_Serie ms { get; set; } = null!;
}
