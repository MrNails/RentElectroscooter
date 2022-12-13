using RentElectroScooter.UI.ViewModels;

namespace RentElectroScooter.UI
{
    public partial class MainPage : ContentPage
    {
        private static readonly uint TranslationTime = 800u;

        private readonly MainPageVM _mainPageVM;

        public MainPage(MainPageVM mainPageVM)
        {
            InitializeComponent();
            _mainPageVM = mainPageVM;

            this.BindingContext = _mainPageVM;        
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await _mainPageVM.LoadElectroScootersCommand.ExecuteAsync(null);
        }

        private void AvailableElectroScooters_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender is not ListView listView)
                return;

            _mainPageVM.SetSelectedItemCommand.Execute(listView.SelectedItem); 
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            MenuGrid.TranslationX = -MenuGrid.Width;
            MenuGrid.ZIndex = 1;
            SideMenuButton.IsEnabled = false;

            _ = MenuGrid.TranslateTo(0, 0, TranslationTime, Easing.CubicIn)
                .ConfigureAwait(false);
            await ContentGrid.FadeTo(0.7, TranslationTime, Easing.CubicIn);
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (MenuGrid.TranslationX < 0)
                return;

            _ = MenuGrid.TranslateTo(-MenuGrid.Width, 0, TranslationTime /2, Easing.CubicIn)
                .ConfigureAwait(false);
            await ContentGrid.FadeTo(1, TranslationTime / 2, Easing.CubicIn);

            MenuGrid.ZIndex = -1;
            SideMenuButton.IsEnabled = true;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
        {
            FrontContentViewContainer.ZIndex = -1;
            FrontContentViewContainer.IsVisible = false;
        }
    }
}