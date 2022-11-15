using contactBook.Core.IRepository;
using contactBook.Data;
using Microsoft.EntityFrameworkCore;

namespace contactBook.Core.Repositories
{
    //Generic class Implementation
    public class GenericRepository <T> :IGenericRepository<T> where T : class    
    {
        //Adding the Application DbContext
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        //intializing the fields above in  a constructor
        public GenericRepository(
            ApplicationDbContext context, ILogger logger
            )
        {
            _context = context;
            _logger = logger;
            this._dbSet = context.Set<T>();
        }

        public  virtual async Task<IEnumerable<T>> GetAll()
        {
           return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public virtual  Task<bool> UpdateUser(T entity)
        {
           throw new NotImplementedException();
           
        }
    }
}
