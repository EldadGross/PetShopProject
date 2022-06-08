using PetShop.Data.Model;
using PetShop.Data.Repository;
using PetShop.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Services
{
    public class CategoryService : ICategoryService<Category>
    {
        public IRepository<Category> Repository { get; }
        public CategoryService(IRepository<Category> repository)
        {
            Repository = repository;
        }

        public async Task<Category> Get(int id)
        {
            var category = await Repository.Get(id);
            return category;
        }

        public IQueryable<Category> GetAll()
        {
            var categories = Repository.GetAll();   
            return categories;  
        }

        public async Task<Category> Save(Category entity)
        {
            var category = entity;
            await Repository.Save(category);
            return category;
        }

        public async Task<Category> Delete(int id)
        {
            var category = await Repository.Get(id);
            await Repository.Delete(category.CategoryId);
            return category;
        }

        public void Add(Category entity)
        {
            Repository.Add(entity);
        }
    }
}
