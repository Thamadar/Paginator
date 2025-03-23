using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;

namespace Paginator.UI.Avalonia.Controls;

public interface IPaginatorBase<T> : IPaginator
{
	  
	/// <summary>
	/// Отображаемый результат Paginator'а.
	/// </summary>
	ObservableCollection<T> OutputItems { get; } 

}
