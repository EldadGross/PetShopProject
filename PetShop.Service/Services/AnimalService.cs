using Microsoft.EntityFrameworkCore;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Data.Repository;
using PetShop.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service
{
    public class AnimalService : IAnimalService<Animal>
    {
        public IRepository<Animal> Repository { get; }
        private readonly IRepository<Comment> commentRepository;   

        public AnimalService(IRepository<Animal> repository, IRepository<Comment> commentRepository)
        {
            Repository = repository;
            this.commentRepository = commentRepository; 
        }
        public void Add(Animal entity)
        {
            Repository.Add(entity);
        }

        public async Task<Animal> Delete(int id)
        {
            var animal = await Repository.Get(id);
            await Repository.Delete(animal.AnimalId);
            return animal;
        }

        public async Task<Animal> Get(int id)
        {
            var animal = await Repository.Get(id);
            return animal;
        }

        public IQueryable<Animal> GetAll()
        {
            var animals = Repository.GetAll();
            return animals; 
        }

        public async Task<Animal> Save(Animal entity)
        {
            await Repository.Save(entity);
            return entity;
        }

        public async Task<IEnumerable<Animal>> GetTwoPopulareAnimals()
        {
            var animals = Repository.GetAll();
            var twoPopulare = animals.OrderByDescending(a => a.Comments.Count)
                .Take(2).Include(a => a.Comments).ToListAsync();
            return await twoPopulare;
        }

        public async Task<IEnumerable<Animal>> GetAnimalsCategory(int id)
        {
            var animalsByCategory = GetAll().Where(a => a.CategoryId == id).ToListAsync();
            return await animalsByCategory;
        }
    }
}
