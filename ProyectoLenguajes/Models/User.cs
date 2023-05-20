using System;
using System.Collections.Generic;

namespace ProyectoLenguajes.Models;

public partial class User
{
    public int user_id { get; set; }

    public string username { get; set; } = null!;

    public string email { get; set; } = null!;

    public string tipo { get; set; } = null!;
}
