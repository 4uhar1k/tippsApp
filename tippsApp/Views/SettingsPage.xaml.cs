
namespace tippsApp;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    public async void goBack(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

}