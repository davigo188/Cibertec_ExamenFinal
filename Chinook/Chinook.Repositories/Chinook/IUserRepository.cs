using Chinook.Models;

namespace Chinook.Repositories.Chinook
{
    public interface IUserRepository: IRepository<User>
    {
        User ValidateUser(string email, string password);
    }
}
