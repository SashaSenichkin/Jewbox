using Jewbox.Models;

namespace Jewbox.Repositories;

public class UserRepository : IUserRepository
{
    private readonly User[] _userStorage =
    [
        new ()
        {
            Id = 0,
            OrgName = "FDDFSDT",
            OrgCode = "HJKLJI",
            NameCode = "SSDFCV",
            Name = "QWE@#$",
            Email = "probpalataalex@gmail.com",
            Phone = "LKHL",
        }, 
        new ()
        {
            Id = 1,
            OrgName = "Индивидуальный предприниматель Литвинов Александр Геннадиевич",
            OrgCode = "ИП710100351300000",
            NameCode = "ЦМЛИ",
            Name = "Литвинов Александр Геннадиевич",
            Email = "probpalataalex@gmail.com",
            //Email = "info@obruchalka71.ru",
            Phone = "89056248476",
        }, 
        new ()
        {
            Id = 2,
            OrgName = "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ \"ЮВЕЛИРНАЯ МАСТЕРСКАЯ ЛИТВИНОВЫХ\"",
            OrgCode = "ЮЛ7101036266",
            NameCode = "Цмук",
            Name = "Литвинов Алексей Геннадьевич",
            Email = "probpalataalex@gmail.com",
            //Email = "lag1503811@mail.ru",
            Phone = "89056209756",
        },
    ];

    public IReadOnlyList<User> GetUsers()
    {
        return _userStorage;
    }

    public User GetUserById(int id)
    {
        return _userStorage[id];
    }
}
