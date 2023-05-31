namespace ProyectoLenguajes.Models;

public partial class Episode
{
    public int episode_id { get; set; }

    public string title { get; set; } = null!;

    public int duration { get; set; }

    public int episode_number { get; set; }

    public int movies_series_id { get; set; }
}
