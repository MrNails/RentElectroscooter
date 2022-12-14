using RentElectroScooter.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Views.Pages
{
    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage(UserVM userVM)
        {
            InitializeComponent();

            BindingContext = userVM;
        }
    }
}