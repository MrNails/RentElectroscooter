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

        public UserVM UserVM => (UserVM)BindingContext;

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