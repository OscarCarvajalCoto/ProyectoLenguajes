namespace ProyectoLenguajes.Models
{
    public class Message
    {
        public string Text { get; set; }
        public string Tipo { get; set; }
        
    }

    public enum Alerta
    {
        danger,
        success,
        warning,
        darger

    };

}



