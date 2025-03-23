
using ReactiveUI;
using DynamicData;

using System;
using System.Collections.ObjectModel; 
using System.Reactive.Linq;

using Paginator.UI.Avalonia.Extensions;
using System.Reactive;

namespace Paginator.UI.Avalonia.Controls;

public abstract class PaginatorBase<T> : ViewModelBase, IPaginatorBase<T>
{
	private readonly ReadOnlyObservableCollection<T> _totalItems = new(new());
	private readonly ObservableCollection<T> _outputItems = new(new());

	private uint _displayedCount;
	private uint _selectedPage = 1;
	private uint _pagesCount; 

	/// <inheritdoc/> 
	public uint DisplayedCount
	{
		get => _displayedCount;
		set => this.RaiseAndSetIfChanged(ref _displayedCount, value);
	}

	/// <inheritdoc/> 
	public uint SelectedPage
	{
		get => _selectedPage;
		set => this.RaiseAndSetIfChanged(ref _selectedPage, value);
	}

	/// <inheritdoc/> 
	public uint PagesCount
	{
		get => _pagesCount;
		set => this.RaiseAndSetIfChanged(ref _pagesCount, value);
	}

	/// <inheritdoc/> 
	public virtual double ItemHeight { get; } = 45;

	/// <inheritdoc/> 
	public ReactiveCommand<uint, Unit> SelectPageCommand { get; }

	/// <inheritdoc/> 
	public ObservableCollection<T> OutputItems => _outputItems;

	public PaginatorBase(IObservable<IChangeSet<T>> totalItemsObservable)
	{  
		var itemsObserver = totalItemsObservable
			.Bind(out _totalItems);

		itemsObserver
				.Do(x => UpdatePaginator())
				.Subscribe()
				.AddTo(_disposables);
		 
		this.WhenAnyValue(x => x.DisplayedCount, x => x.SelectedPage)
			.Do(x => UpdatePaginator())
			.Subscribe()
			.AddTo(_disposables);

		SelectPageCommand = ReactiveCommand.Create<uint, Unit>((page) =>
		{
			SelectedPage = page;
			return Unit.Default;
		}); 
	} 

	/// <summary>
	/// Обновление Paginator и output-коллекции контейнера, который отображает Items.
	/// </summary>
	public void UpdatePaginator()
	{
		UpdatePagesCount();

		var startIndex = (SelectedPage-1) * DisplayedCount;
		var endIndex   = (SelectedPage * DisplayedCount) != 0 ? (SelectedPage * DisplayedCount) - 1 : 0;

		_outputItems.Clear();
		if(startIndex != endIndex)
		{
			endIndex = endIndex > _totalItems.Count - 1 ? (uint)_totalItems.Count - 1 : endIndex;
			_outputItems.AddRange(_totalItems.GetRange((int)startIndex, (int)endIndex));
		} 
	}

	/// <inheritdoc/> 
	public void SelectPage(uint selectedPage) => SelectedPage = selectedPage > PagesCount ? PagesCount : selectedPage;

	/// <summary>
	/// Обновление страниц в Paginator.
	/// </summary>
	private void UpdatePagesCount()
	{ 
		PagesCount = _totalItems.Count > 0 && DisplayedCount > 0 ?
			         (uint)Math.Ceiling((double)(_totalItems.Count) / DisplayedCount)
			         : 1;

		if(SelectedPage > PagesCount)
		{
			SelectedPage = 1;
		}
	} 
}
