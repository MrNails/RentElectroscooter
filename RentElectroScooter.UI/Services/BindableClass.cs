using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Services
{
    public abstract partial class BindableClass : ObservableObject
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBussy))]
        private bool _isBusy;
        
        public bool IsNotBussy { get; }
    }
}
