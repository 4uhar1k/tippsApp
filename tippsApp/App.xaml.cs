namespace tippsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new ContentPage();

            //MainPage = new NavigationPage(new MainPage());

            //MainPage = ;
            MainPage = new AppShell();

        }
    }
}
