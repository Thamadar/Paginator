using System; 
using System.Collections.ObjectModel;
using System.Linq; 

namespace Paginator.UI.Avalonia.Extensions;

public static class ObservableCollectionExtensions
{
	public static ObservableCollection<T> GetRange<T>(this ObservableCollection<T> collection, int startIndex, int endIndex)
	{
		if(startIndex < 0 || endIndex >= collection.Count || startIndex > endIndex)
			throw new ArgumentOutOfRangeException("Индексы вне диапазона");

		ObservableCollection<T> result = new ObservableCollection<T>(
			collection.Skip(startIndex).Take(endIndex - startIndex + 1)
		);

		return result;
	}
	public static ObservableCollection<T> GetRange<T>(this ReadOnlyObservableCollection<T> collection, int startIndex, int endIndex)
	{
		if(startIndex < 0 || endIndex >= collection.Count || startIndex > endIndex)
			throw new ArgumentOutOfRangeException("Индексы вне диапазона");

		ObservableCollection<T> result = new ObservableCollection<T>(
			collection.Skip(startIndex).Take(endIndex - startIndex + 1)
		);

		return result;
	}
}
