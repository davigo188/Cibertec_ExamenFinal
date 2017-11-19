using Chinook.Repositories.Chinook;
using Chinook.UnitOfWork;

namespace Chinook.Repositories.Dapper.Chinook
{
    public class ChinookUnitOfWork: IUnitOfWork
    {

        public ChinookUnitOfWork(string connectionString)
        {
            Customers = new CustomerRepository(connectionString);
            Album = new AlbumRepository(connectionString);
            Artist = new ArtistRepository(connectionString);
            Employee = new EmployeeRepository(connectionString);
            Users = new UserRepository(connectionString);
        }

        public ICustomerRepository Customers { get; private set; }

        public IAlbumRepository Album { get; private set; }

        public IArtistRepository Artist { get; private set; }

        public IEmployeeRepository Employee { get; private set; }

        public IUserRepository Users { get; private set; }
    }
}
