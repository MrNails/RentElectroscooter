using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInContentView : ContentView, INotifyPropertyChanged
    {
        private IRelayCommand _moveToRegistrationViewCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public SignInContentView()
        {
            InitializeComponent();

            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
        }

        public IRelayCommand MoveToRegistrationViewCommand
        {
            get => _moveToRegistrationViewCommand;
            set
            {
                _moveToRegistrationViewCommand = value;

                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}