using RentElectroScooter.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Views
{
    public partial class RegisterContentView : ContentView
    {
        private Command _moveToAuthorizationViewCommand;

        public RegisterContentView(UserVM userViewModel)
        {
            InitializeComponent();

            BindingContext = userViewModel;

            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
        }

        public Command MoveToAuthorizationViewCommand
        {
            get => _moveToAuthorizationViewCommand;
            set
            {
                _moveToAuthorizationViewCommand = value;

                OnPropertyChanged();
            }
        }
    }
}