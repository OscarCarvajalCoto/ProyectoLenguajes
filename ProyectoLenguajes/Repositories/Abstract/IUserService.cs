namespace ProyectoLenguajes.Repositories.Abstract
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}
