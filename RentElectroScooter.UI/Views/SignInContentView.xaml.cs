using CommunityToolkit.Mvvm.Input;
using RentElectroScooter.UI.ViewModels;
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
    public partial class SignInContentView : ContentView
    {
        private Command _moveToRegistrationViewCommand;

        public SignInContentView(UserVM userViewModel)
        {
            InitializeComponent();

            BindingContext = userViewModel;

            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
        }

        public Command MoveToRegistrationViewCommand
        {
            get => _moveToRegistrationViewCommand;
            set
            {
                _moveToRegistrationViewCommand = value;

                OnPropertyChanged();
            }
        }
    }
}