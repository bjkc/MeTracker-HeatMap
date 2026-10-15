using MeTracker.Views;

namespace MeTracker
{
    public partial class App : Application
    {
        public App(MainView mainView)
        {
            InitializeComponent();

            MainPage = new NavigationPage(mainView)
            {
                BarBackgroundColor = Color.FromArgb("#512BD4"),
                BarTextColor = Colors.White
            };
        }
    }
}
