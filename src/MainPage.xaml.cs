using System.ComponentModel;
using PristonToolsEU.Update;

namespace PristonToolsEU;

public partial class MainPage : ContentPage
{
    private readonly IUpdateChecker _updateChecker;

    public MainPage(MainPageViewModel viewModel, IUpdateChecker updateChecker)
    {
        _updateChecker = updateChecker;
        InitializeComponent();

        BindingContext = viewModel;
        CheckForUpdate();
    }

    private async void CheckForUpdate()
    {
        var updateCheckResult = await _updateChecker.Check();
        if (!updateCheckResult.HasNewUpdate)
            return;
        var userSelection = await DisplayAlert("Update available",
            $"There is an update available: {updateCheckResult.UpdateInfo?.Version} \n" +
            $"Would you like to update?",
            "Yes", "No");
        if (!userSelection || updateCheckResult.UpdateInfo == null)
        {
            return;
        }

        try
        {
            Uri uri = new Uri(updateCheckResult.UpdateInfo.Url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
            await DisplayAlert("Error",
                "An error occured, no browser may be installed.\n" +
                ex,
                "Ok");
        }
    }
}