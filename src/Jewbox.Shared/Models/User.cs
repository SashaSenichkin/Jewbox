namespace Jewbox.Models;

public class User
{
    public int Id { get; init; }

    /// <summary>
    /// Наименование юрлица, ИП, ФИО художника-ювелира
    /// </summary>
    public required string OrgName { get; init; }

    /// <summary>
    /// Номер спецучета
    /// </summary>
    public required string OrgCode { get; init; }

    /// <summary>
    /// Именник
    /// </summary>
    public required string NameCode { get; init; }

    /// <summary>
    /// ФИО (как в паспорте)
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string Phone { get; init; }
}
