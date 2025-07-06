using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebappTwitterApi.Data.Entity;

namespace WebappTwitterApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):IdentityDbContext<User>(options)
    {
       

    }
}
