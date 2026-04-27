using Jewbox.Models;

namespace Jewbox.Repositories;

public interface IUserRepository
{
    IReadOnlyList<User> GetUsers();
    User GetUserById(int id);
}
