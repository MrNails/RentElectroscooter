using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.DAL.Repositories;

namespace RentElectroScooter.API.Services
{
    public static class SQLHelpers
    {
        public static string AddColumnBracket(string columnName, AvailableDatabases availableDatabase) => availableDatabase switch
        {
            AvailableDatabases.MSSQL => $"[{columnName}]",
            _ => throw new ArgumentException($"Database {availableDatabase} is unavailable"),
        };

        public static string GetComprasionOperation(ComprasionType comprasionType) => comprasionType switch
        {
            ComprasionType.AND => "AND",
            ComprasionType.OR => "OR",
            _ => throw new ArgumentException($"ComprasionType {comprasionType} is unavailable"),
        };
    }
}
