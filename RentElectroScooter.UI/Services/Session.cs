using CommunityToolkit.Mvvm.ComponentModel;
using RentElectroScooter.CoreModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Services
{
    [ObservableObject]
    public sealed partial class Session
    {
        [ObservableProperty]
        private UserProfile _userProfile;

        public string Jwt { get; set; }
    }
}
