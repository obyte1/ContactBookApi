using contactBook.Core.IRepository;

namespace contactBook.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        //A method that is responsible for communicating our changes to the database
        Task CompleteAsync();
    }
}
