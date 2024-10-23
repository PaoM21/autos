using Autos.Models;
using Microsoft.EntityFrameworkCore;

namespace Autos.Data
{
    public static class Seed
    {
        public static void DbInitializer(DataBase context)
        {
            context.Database.EnsureCreated();

            if (!context.MarcasAutos.Any())
            {
                var marcas = new List<MarcasAutos>
                {
                    new MarcasAutos { Marca = "Toyota" },
                    new MarcasAutos { Marca = "Ford" },
                    new MarcasAutos { Marca = "Honda" }
                };

                context.MarcasAutos.AddRange(marcas);
                context.SaveChanges();
            }
        }
    }
}
