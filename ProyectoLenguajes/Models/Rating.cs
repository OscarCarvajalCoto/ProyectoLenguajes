using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLenguajes.Models;

public partial class Rating
{
    public int rating_id { get; set; }
    [Required(ErrorMessage = "The rating field is required")]
    [RegularExpression("^(?=.*?[1-5]).{1,1}$", ErrorMessage = "It must be a number from 1 to 5")]
    public int rating1 { get; set; }

    public string app_user { get; set; } = null!;

    public int ms_id { get; set; }

    public virtual Movie_Serie ms { get; set; } = null!;
}
