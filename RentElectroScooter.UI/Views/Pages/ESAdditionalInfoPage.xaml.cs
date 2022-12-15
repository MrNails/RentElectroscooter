using RentElectroScooter.CoreModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Views.Pages
{
	[QueryProperty("AdditionalInfo", "AdditionalInfo")]
	public partial class ESAdditionalInfoPage : ContentPage
	{
		public ESAdditionalInfoPage ()
		{
			InitializeComponent ();
		}

		public VehicleData AdditionalInfo
		{
			get => (VehicleData)BindingContext;
			set => BindingContext = value;
		}
	}
}