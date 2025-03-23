using Avalonia;
using Avalonia.Controls.Metadata;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

using ReactiveUI;

using System.Collections.ObjectModel;
using System.Reactive.Linq; 
using System;
using System.Collections.Generic;

using DynamicData;
using Avalonia.Data;

namespace Paginator.UI.Avalonia.Controls;

[TemplatePart("PART_FirstPageButtonIcon", typeof(ButtonIcon))]
[TemplatePart("PART_PagesItemsControl",   typeof(ItemsControl))]
[TemplatePart("PART_LastPageButtonIcon",  typeof(ButtonIcon))]
public class PaginatorControl : TemplatedControl
{

	private ButtonIcon? _firstPageButtonIcon;
	private ItemsControl? _pagesItemsControl;
	private ButtonIcon? _lastPageButtonIcon; 

	private bool _areControlsAvailable;
	  
	public static readonly StyledProperty<uint> StepPageProperty =
		AvaloniaProperty.Register<PaginatorControl, uint>(nameof(StepPage), defaultValue: 5);

	public static readonly StyledProperty<uint> SelectedPageProperty =
		AvaloniaProperty.Register<PaginatorControl, uint>(nameof(SelectedPage));

	public static readonly StyledProperty<uint> PagesCountProperty =
		AvaloniaProperty.Register<PaginatorControl, uint>(nameof(PagesCount));

	public static readonly StyledProperty<uint> MaxPagesProperty =
		AvaloniaProperty.Register<PaginatorControl, uint>(nameof(MaxPages), defaultValue: 10);

	public static readonly StyledProperty<uint> TotalItemsCountProperty =
		AvaloniaProperty.Register<PaginatorControl, uint>(nameof(TotalItemsCount));

	/// <summary>
	/// Шаг, с который пролистывается список страниц.
	/// </summary>
	public uint StepPage
	{
		get => GetValue(StepPageProperty);
		set => SetValue(StepPageProperty, value);
	}

	/// <summary>
	/// Выбранная страница Paginator'а.
	/// </summary>
	public uint SelectedPage 
	{
		get => GetValue(SelectedPageProperty);
		set => SetValue(SelectedPageProperty, value);
	} 

	/// <summary>
	/// Количество страниц в Paginator.
	/// </summary>
	public uint PagesCount 
	{
		get => GetValue(PagesCountProperty);
		set => SetValue(PagesCountProperty, value);
	}
	 
	/// <summary>
	/// Максимальное отображаемое кол-во страниц.
	/// DO TO: Cделать автоподсчет влезаемых страниц, используя ActualWidth контрола (Bounds.Width)
	/// </summary>
	public uint MaxPages
	{
		get => GetValue(MaxPagesProperty);
		set => SetValue(MaxPagesProperty, value);
	}

	/// <summary>
	/// Количество отображаемых объектов. 
	/// </summary>
	public uint TotalItemsCount
	{
		get => GetValue(TotalItemsCountProperty);
		set => SetValue(TotalItemsCountProperty, value);
	}

	/// <summary>
	/// Command для переключения на первую страницу.
	/// </summary>
	public IReactiveCommand FirstPageCommand { get; }

	/// <summary>
	/// Command для переключения на последнюю страницу.
	/// </summary>
	public IReactiveCommand LastPageCommand { get; }

	public ObservableCollection<uint> PagesItems { get; } 

	public PaginatorControl()
	{
		PagesItems = new ObservableCollection<uint>(); 

		FirstPageCommand = ReactiveCommand.Create(() => { SelectedPage = 1; });
		LastPageCommand  = ReactiveCommand.Create(() => { SelectedPage = PagesCount; });

		this.WhenAnyValue(x => x.PagesCount, x => x.SelectedPage) 
			.Subscribe(x => RefreshPagesItems());
	} 

	protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
	{
		_areControlsAvailable = false;

		base.OnApplyTemplate(e);

		_firstPageButtonIcon = InitFirstPageButtonIconIcon(e.NameScope.Find<ButtonIcon>("PART_FirstPageButtonIcon"));
		_pagesItemsControl   = InitPagesItemsControl(e.NameScope.Find<ItemsControl>("PART_PagesItemsControl"));
		_lastPageButtonIcon  = InitLastPageButtonIconIcon(e.NameScope.Find<ButtonIcon>("PART_LastPageButtonIcon")); 

		_areControlsAvailable = true;
	}

	private ItemsControl InitPagesItemsControl(ItemsControl? control)
	{
		var bind = new Binding(nameof(PagesItems))
		{
			Source = this
		};
		control.Bind(ItemsControl.ItemsSourceProperty, bind);

		return control;
	}

	private ButtonIcon InitFirstPageButtonIconIcon(ButtonIcon? control)
	{ 
		control.Command = (System.Windows.Input.ICommand?)FirstPageCommand;

		return control;
	}
	 
	private ButtonIcon InitLastPageButtonIconIcon(ButtonIcon? control)
	{
		control.Command = (System.Windows.Input.ICommand?)LastPageCommand;

		return control;
	}

	/// <summary>
	/// Обновление страниц в Paginator.
	/// </summary>
	private void RefreshPagesItems()
	{ 
		var newPagesItems = new List<uint>();

		if(MaxPages <= PagesCount)
		{  
			uint startIndex = SelectedPage + MaxPages > PagesCount ?
							  PagesCount + 1 - MaxPages :
							  SelectedPage;

			for(uint i = startIndex; i < MaxPages + startIndex; i++)
			{
				newPagesItems.Add(i);
			}
		} 
		else
		{
			for(uint i = 1; i < PagesCount + 1; i++)
			{
				newPagesItems.Add(i);
			}
		}
		PagesItems.Clear();
		PagesItems.AddRange(newPagesItems);
	}
}
