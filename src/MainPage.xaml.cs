﻿using System.ComponentModel;

namespace PristonToolsEU;

public partial class MainPage : ContentPage
{
	// int count = 0;

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

		// viewModel.PropertyChanged += OnPropertyChanged;
	}

	// private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
	// {
	// 	e.PropertyName
	// }

	// private void OnCounterClicked(object sender, EventArgs e)
	// {
	// 	count++;
	//
	// 	if (count == 1)
	// 		CounterBtn.Text = $"Clicked {count} time";
	// 	else
	// 		CounterBtn.Text = $"Clicked {count} times";
	//
	// 	SemanticScreenReader.Announce(CounterBtn.Text);
	// }
}
