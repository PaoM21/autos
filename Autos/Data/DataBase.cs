using Microsoft.EntityFrameworkCore;
using Autos.Models;

namespace Autos.Data
{
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
        }
        public DbSet<MarcasAutos> Autos => Set<MarcasAutos>();
    }
}
