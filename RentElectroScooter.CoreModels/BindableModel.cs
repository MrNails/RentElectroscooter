using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RentElectroScooter.CoreModels
{
    public abstract class BindableModel : INotifyPropertyChanged, IDataErrorInfo
    {
        protected readonly Dictionary<string, string> m_errors = new Dictionary<string, string>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public string this[string columnName] => m_errors.TryGetValue(columnName, out var value) ? value : String.Empty;

        [JsonIgnore]
        [XmlIgnore]
        public string? Error => m_errors.FirstOrDefault(e => !string.IsNullOrEmpty(e.Value)).Value;

        protected void OnPropertyChanged([CallerMemberName] string prop = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        
    }
}
