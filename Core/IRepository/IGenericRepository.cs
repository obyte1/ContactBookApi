using System.Collections;

namespace contactBook.Core.IRepository
{
    //A generic Repo for the repository, where other repos will inherit from
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid Id);
        Task<bool> UpdateUser(T entity);

    }
}
