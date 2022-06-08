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
    public class CommentRepository : IRepository<Comment>
    {
        private readonly PetShopDataContext context;
        public CommentRepository(PetShopDataContext context)
        {
            this.context = context;
        }

        public void Add(Comment entity)
        {
            context.Comments.Add(entity);  
            var animal = context.Animals.FirstOrDefault
                (a => a.AnimalId == entity.AnimalId);
            animal!.Comments.Add(entity);
            context.SaveChanges();  
        }

        public async Task<Comment> Delete(int id)
        {
            var comment = await Get(id);
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> Get(int id)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
            return comment!;
        }

        public IQueryable<Comment> GetAll()
        {
            return context.Comments;
        }

        public async Task<Comment> Save(Comment entity)
        {
            context.Comments.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
