using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoLenguajes.Models.Domain
{
    public class DataBaseSecurityContext : IdentityDbContext<ApplicationUser>
    {
        public DataBaseSecurityContext(DbContextOptions<DataBaseSecurityContext> options) : base(options)
        {

        }

    }
}
