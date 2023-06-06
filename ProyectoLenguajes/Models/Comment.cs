using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLenguajes.Models;

public partial class Comment
{
    public int comment_id { get; set; }
    [Required(ErrorMessage = "The comment field is required")]
    public string comment1 { get; set; } = null!;

    public string app_user { get; set; } = null!;

    public int ms_id { get; set; }

    public virtual Movie_Serie ms { get; set; } = null!;
}
