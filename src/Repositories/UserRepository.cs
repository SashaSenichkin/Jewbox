using Jewbox.Models;

namespace Jewbox.Repositories;

public class UserRepository : IUserRepository
{
    private readonly User[] _userStorage =
    [
        new ()
        {
            Id = 0,
            OrgName = "Индивидуальный предприниматель Литвинов Александр Геннадиевич",
            OrgCode = "ИП710100351300000",
            NameCode = "ЦМЛИ",
            Name = "Литвинов Александр Геннадиевич",
            Email = "info@obruchalka71.ru",
            Phone = "89056248476",
        }, 
        new ()
        {
            Id = 1,
            OrgName = "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ \"ЮВЕЛИРНАЯ МАСТЕРСКАЯ ЛИТВИНОВЫХ\"",
            OrgCode = "ЮЛ7101036266",
            NameCode = "Цмук",
            Name = "Литвинов Алексей Геннадьевич",
            Email = "lag1503811@mail.ru",
            Phone = "89056209756",
        },
    ];

    public User GetUserById(int id)
    {
        return _userStorage[id];
    }
}