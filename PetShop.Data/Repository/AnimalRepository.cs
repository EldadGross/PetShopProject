using Microsoft.EntityFrameworkCore;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repository
{
    public class AnimalRepository : IRepository<Animal>
    {
        private readonly PetShopDataContext context;

        public AnimalRepository(PetShopDataContext context)
        {
            this.context = context;
        }

        public void Add(Animal entity)
        {
            context.Animals.Add(entity);
            context.SaveChanges();
        }

        public async Task<Animal> Delete(int id)
        {
            var animal = await Get(id);
            context.Animals.Remove(animal);
            await context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> Get(int id)
        {
            var animal = await context.Animals.Include(c => c.Comments)
                .FirstOrDefaultAsync(animal => animal.AnimalId == id);
            return animal!;
        }

        public IQueryable<Animal> GetAll()
        {
            return context.Animals;
        }

        public async Task<Animal> Save(Animal entity)
        {
            context.Animals.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
