using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using MeTracker.ViewModels;
using MeTracker.Controls;

namespace MeTracker.Views
{
    public partial class MainView : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            HeatMapCanvas.Drawable = new HeatMapDrawable();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await _viewModel.LoadLocationsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during map load: {ex.Message}");
            }
        }
    }
}
