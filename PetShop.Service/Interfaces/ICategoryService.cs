namespace PetShop.Service.Interfaces
{
    public interface ICategoryService<T> where T : class 
    {
        Task<T> Get(int id);
        IQueryable<T> GetAll();
        Task<T> Save(T entity);
        Task<T> Delete(int id);
        void Add(T entity);
    }
}
