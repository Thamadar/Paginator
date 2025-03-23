using DynamicData; 

namespace Paginator.Client.Avalonia.Services;

public interface ITestDataService
{
	/// <summary> 
	/// Подключение к тестовому списку объектов для таблицы, имитирующего данные с X-сервера.
	/// </summary> 
	IObservable<IChangeSet<string>> ConnectToTestDataItems();

	/// <summary>
	/// Обновление тестового списка.
	/// </summary> 
	Task<bool> UpdateTestDataItems();

}

public class TestDataService : ITestDataService
{

	private readonly ISourceList<string> _testDataItems = new SourceList<string>();

	public TestDataService()
	{

	}

	/// <inheritdoc/>
	public IObservable<IChangeSet<string>> ConnectToTestDataItems()
	{
		return _testDataItems.Connect();
	}

	/// <inheritdoc/>
	public Task<bool> UpdateTestDataItems()
	{
		var result = default(bool);

		var newStrings = new List<string>();
		var rnd        = new Random();
		var rndCount   = rnd.Next(50, 500);
		for(int i = 0; i < 150; i++)
		{ 
			newStrings.Add($"TEST_STRING #{i + 1}");
		}

		_testDataItems.Clear();
		_testDataItems.AddRange(newStrings);
		  
		result = true;

		return Task.FromResult(result);  
	}
}
