using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using System.Collections.Generic;
using DynamicData;

namespace BrowserTest.ViewModels;

public class MainViewModel : ViewModelBase
{
	public ReactiveCommand<Unit, Unit> ClickCommand { get; }
	public ReactiveCommand<object, Unit> EditCommand { get; set; }
	public ReactiveCommand<int, Unit> ExpandCommand { get; set; }

	public MainViewModel()
	{
		ClickCommand = ReactiveCommand.Create(() =>
		{
			var folder = "D:\\Csharp\\test";

			File.Create(Path.Combine(folder, Guid.NewGuid().ToString()));
			Directory.GetFiles(folder).ToList().ForEach(f => { System.Diagnostics.Debug.WriteLine(f); });
			;
		});

		EditCommand = ReactiveCommand.CreateFromTask<object, Unit>(EditCommandExecuted);
		ExpandCommand = ReactiveCommand.CreateFromTask<int, Unit>(ExpandExecute);

		var accounts = new List<Profile>
		{
			new Profile{ Id = 1},
			new Profile{ Id = 2},
			new Profile{ Id = 3},
			new Profile{ Id = 4},
		};

		Accounts.AddRange(accounts);


	}

	private int _selectedAccountIdx { get; set; }
	public int SelectedAccountIdx
	{
		get
		{
			return _selectedAccountIdx;
		}
		set
		{
			_selectedAccountIdx = value;
		}
	}

	public ObservableCollection<Profile> Accounts { get; } = new ObservableCollection<Profile>();




	private async Task<Unit> EditCommandExecuted(object p)
	{
		// p contains "test77"

		return await Task.FromResult(Unit.Default);
	}
	private async Task<Unit> ExpandExecute(int p)
	{
		// p contains "test77"
		System.Diagnostics.Debug.WriteLine(p);
		return await Task.FromResult(Unit.Default);
	}
#pragma warning disable CA1822 // Mark members as static
	public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
}
