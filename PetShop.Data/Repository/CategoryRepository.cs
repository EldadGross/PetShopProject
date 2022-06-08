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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly PetShopDataContext context;
        public CategoryRepository(PetShopDataContext context)
        {
            this.context = context; 
        }

        public void Add(Category entity)
        {
            var category = entity;
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public async Task<Category> Delete(int id)
        {
            var category = await Get(id);
            context.Categories.Remove(category);
            return category;
        }

        public async Task<Category> Get(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);             
            return category;    
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;  
        }

        public async Task< Category> Save(Category entity)
        {
            context.Categories.Update(entity);  
            await context.SaveChangesAsync();   
            return entity;
        }
    }
}
