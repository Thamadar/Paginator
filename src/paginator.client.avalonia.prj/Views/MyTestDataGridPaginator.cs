using DynamicData; 
using Paginator.UI.Avalonia.Controls; 

namespace Paginator.Client.Avalonia.Views;
public class MyTestDataGridPaginator : PaginatorBase<string>
{
	/// <inheritdoc/>
	public override double ItemHeight => 45;

	/// <summary>
	/// Конструктор для наследника PaginatorBase к MyTestDataGrid.
	/// </summary> 
	public MyTestDataGridPaginator(
			IObservable<IChangeSet<string>> observable)
		: base(observable)
	{ }
}
