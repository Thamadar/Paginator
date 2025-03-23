using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Paginator.UI.Avalonia.Controls;
public interface IPaginator
{
	/// <summary>
	/// Количество влезаемых Item'ов в отображаемый контрол.
	/// </summary>
	uint DisplayedCount { get; set; }

	/// <summary>
	/// Выбранная страница Paginator'а.
	/// </summary>
	uint SelectedPage { get; set; }

	/// <summary>
	/// Количество страниц в Paginator.
	/// </summary>
	uint PagesCount { get; set; }

	/// <summary>
	/// Высота Item'а в отображаемом контроле.
	/// </summary>
	double ItemHeight { get; }

	/// <summary>
	/// Выбор страницы.
	/// </summary>
	void SelectPage(uint selectedPage);

	/// <summary>
	/// Command для выбора навигации Paginator.
	/// </summary>
	ReactiveCommand<uint, Unit> SelectPageCommand { get; }
}
