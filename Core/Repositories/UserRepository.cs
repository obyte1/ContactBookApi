using contactBook.Core.IRepository;
using contactBook.Data;
using contactBook.Model;
using Microsoft.EntityFrameworkCore;

namespace contactBook.Core.Repositories
{
    //we inherit from both the Generic repo and Iuser Repo
    //so that we can be exposed to all the methods in both class
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(
            ApplicationDbContext context,
            ILogger logger
            ) : base(context, logger)//because we are inheriting from the generic repo we have to pass it back to the base
        {

        }
        //because this method already exist in the parent class we need to override it
        public override async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} All Methods erro", typeof(UserRepository));
                //once it fails return the list of users back
                return new List<User> { };
            }
        }
        public override async Task<bool> UpdateUser(User entity)
        {

            try
            {
                //check if the user exists in the databse and if it exist return the first result
                var existingUser = await _dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();
                if (existingUser == null)
                    return await Add(entity);
                //if the user exist then update it
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Update Methods erro", typeof(UserRepository));
                //once it fails return the list of users back
                return false;
            }

        }

        public override async Task<bool> Delete(Guid Id)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.Id == Id)
                                  .FirstOrDefaultAsync();
                if(exist != null)
                {
                    _dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Delete Method error", typeof(UserRepository));
                return false;
            }
        }
    }
}
