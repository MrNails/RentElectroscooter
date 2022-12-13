using RentElectroScooter.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.ViewModels
{
    public abstract class ItemManagerVM<T> : BindableClass
        where T : class
    {
        private readonly ObservableCollection<T> _items;

        private int _currentIndex;

        public ItemManagerVM()
        {
            _items = new ObservableCollection<T>();
        }

        public ObservableCollection<T> Items => _items;

        public T CurrentElement => CurrentIndex == -1 ? null : _items[CurrentIndex];

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                OnPropertyChanging();
                OnPropertyChanging(nameof(CurrentElement));

                _currentIndex = value >= -1 && value < _items.Count 
                    ? value 
                    : throw new ArgumentOutOfRangeException(nameof(value), "Value must be in range [-1;Items.Count()]");

                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentElement));
            }
        }
    }
}
