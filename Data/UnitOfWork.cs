using contactBook.Core.IConfiguration;
using contactBook.Core.IRepository;
using contactBook.Core.Repositories;

namespace contactBook.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
     
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; private set; }

       

        public UnitOfWork(
         
            ILoggerFactory loggerFactory, 
            ApplicationDbContext context
       
            )
        {
           // _configuration = configuration;
            _logger = loggerFactory.CreateLogger("logs");
            _context = context;

            Users = new UserRepository(_context, _logger);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
             _context.Dispose();
        }

       
    }

    
}
