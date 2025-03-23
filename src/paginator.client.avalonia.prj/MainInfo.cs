using Autofac;
using System.Diagnostics;

namespace Paginator.Client.Avalonia;
public class MainInfo
{
	private readonly object _lockerObjects  = new object();
	private readonly object _lockerServices = new object();
	private IContainer _container;

	public MainInfo()
	{

	}

	public static MainInfo DesignMainInfo => new();

	public void SetIContainer(IContainer container) => _container = container;

	public T GetItem<T>()
		where T : class
	{
		var resultObject = default(T);

		try
		{
			lock(_lockerObjects)
			{
				if(_container.TryResolve(typeof(T), out var tempObject)
					&& tempObject is T targetType)
				{
					resultObject = targetType;
				}
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine($"Error. Can't get object. Type error: {ex.GetType().Name}. Message: {ex.Message}");
		}

		return resultObject; 
	}
}
