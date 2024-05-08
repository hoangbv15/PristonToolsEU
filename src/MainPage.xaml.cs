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
		DisplayAlert("Update available", 
			$"There is an update available: {updateCheckResult.UpdateInfo?.Version}", 
			"OK");
	}
}

