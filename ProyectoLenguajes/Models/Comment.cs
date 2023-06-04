using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class Comment
{
    public int comment_id { get; set; }

    public string comment1 { get; set; } = null!;

    public string app_user_id { get; set; } = null!;

    public int ms_id { get; set; }

    public virtual Movie_Serie ms { get; set; } = null!;
}
