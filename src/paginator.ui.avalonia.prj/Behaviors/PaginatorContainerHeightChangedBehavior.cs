using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using Paginator.UI.Avalonia.Controls;
using System;

namespace Paginator.UI.Avalonia.Behaviors;
/// <summary>
/// Behavior, который подсчитывает сколько влезает Item'ов в Container - необходимо для PaginatorBase.
/// </summary>
public sealed class PaginatorContainerHeightChangedBehavior : Behavior<ItemsControl>
{

	public static readonly StyledProperty<double> ItemHeightProperty =
		AvaloniaProperty.Register<PaginatorControl, double>(nameof(ItemHeight));
	 
	/// <summary>
	/// Высота Item'а в отображаемом контроле.
	/// </summary>
	public double ItemHeight
	{
		get => GetValue(ItemHeightProperty);
		set => SetValue(ItemHeightProperty, value);
	}

	protected override void OnAttached()
	{ 
		AssociatedObject.SizeChanged += OnPaginatorContainerHeightChanged;
		base.OnAttached();
	} 

	protected override void OnDetaching()
	{
		AssociatedObject.SizeChanged -= OnPaginatorContainerHeightChanged;
		base.OnDetaching();
	}
	 
	private void OnPaginatorContainerHeightChanged(object? sender, SizeChangedEventArgs e)
	{
		if(AssociatedObject.DataContext is IPaginator paginatorVM)
		{
			paginatorVM.DisplayedCount = (uint)Math.Floor((decimal)(AssociatedObject.Bounds.Height / paginatorVM.ItemHeight));
			e.Handled = true;
		}
	} 
}
