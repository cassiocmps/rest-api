using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // a espeficicacao do tipo de DbContextOptions é importante pois temos mais de um contexto nessa api
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}