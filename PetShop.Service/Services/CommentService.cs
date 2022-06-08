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
    public class CommentService : ICommentService<Comment>
    {
        public IRepository<Comment> Repository;
        public CommentService(IRepository<Comment> repository)
        {
            Repository = repository;
        }

        public void Add(Comment entity)
        {
            Repository.Add(entity);
        }

        public async Task<Comment> Delete(int id)
        {
            var comment = await Repository.Get(id);
            await Repository.Delete(comment.CommentId);
            return comment;
        }

        public async Task<Comment> Get(int id)
        {
            var comment = await Repository.Get(id);   
            return comment;
        }

        public IQueryable<Comment> GetAll()
        {
            return Repository.GetAll();
        }

        public async Task<Comment> Save(Comment entity)
        {
            var comment = entity;
            await Repository.Save(comment);
            return comment; 
        }
    }
}
