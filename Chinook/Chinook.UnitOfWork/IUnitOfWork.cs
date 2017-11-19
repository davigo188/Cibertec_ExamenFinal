using Chinook.Repositories.Chinook;

namespace Chinook.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IAlbumRepository Album { get; }
        IArtistRepository Artist { get; }
        IEmployeeRepository Employee { get; }
        IUserRepository Users { get; }
    }
}
