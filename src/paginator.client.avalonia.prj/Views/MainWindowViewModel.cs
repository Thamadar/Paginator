using DynamicData;
using Paginator.Client.Avalonia.Services;
using Paginator.UI.Avalonia;
using Paginator.UI.Avalonia.Extensions;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Paginator.Client.Avalonia.Views;

public sealed partial class MainWindowViewModel : ViewModelBase
{
	private readonly MainInfo _mainInfo;
	private readonly ITestDataService _testDataService;

	private ReadOnlyObservableCollection<string> _testList = new(new());
	 
	public ReadOnlyObservableCollection<string> TestList => _testList;

	/// <summary>
	/// PaginatorVM.
	/// </summary>
	public MyTestDataGridPaginator MyTestDataGridPaginator { get; }

	public MainWindowViewModel()
		:this(MainInfo.DesignMainInfo, new TestDataService())
	{

	}

	public MainWindowViewModel(
		MainInfo mainInfo,
		ITestDataService testDataService) 
	{
		_mainInfo        = mainInfo;
		_testDataService = testDataService;

		var dataObservable = _testDataService
			.ConnectToTestDataItems();

		dataObservable
				.ObserveOn(RxApp.MainThreadScheduler)
				.Bind(out _testList)
				.Subscribe()
				.AddTo(_disposables);

		MyTestDataGridPaginator = new MyTestDataGridPaginator(dataObservable);
	}


	public async Task LoadMainWindow()
	{
		await _testDataService.UpdateTestDataItems();
	} 
}
