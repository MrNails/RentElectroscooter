using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RentElectroScooter.CoreModels.DTO
{
    public abstract class ValidableModel : IDataErrorInfo
    {
        protected Dictionary<string, string> _errors = new();

        public string this[string columnName] 
            => _errors.TryGetValue(columnName, out var error) ? error : string.Empty;

        [JsonIgnore]
        public string Error => _errors.FirstOrDefault(e => e.Value != string.Empty).Value ?? string.Empty;
    }
}
