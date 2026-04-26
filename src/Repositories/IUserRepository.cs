using Jewbox.Models;

namespace Jewbox.Repositories;

public interface IUserRepository
{
    User GetUserById(int id);
}