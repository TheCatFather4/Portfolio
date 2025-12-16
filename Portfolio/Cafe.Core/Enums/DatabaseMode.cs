namespace Cafe.Core.Enums
{
    /// <summary>
    /// Used for switching between Entity Framework Core or Dapper.
    /// </summary>
    public enum DatabaseMode
    {
        ORM,
        Dapper
    }
}