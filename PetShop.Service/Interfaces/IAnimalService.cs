using PetShop.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Interfaces
{
    public interface IAnimalService <T> where T : class
    {
        Task<T> Get(int id);
        IQueryable<T> GetAll();
        Task<T> Save(T entity);
        Task<T> Delete(int id);
        void Add(T entity);
        Task<IEnumerable<T>> GetTwoPopulareAnimals();
        Task<IEnumerable<T>> GetAnimalsCategory(int id);
    }
}
