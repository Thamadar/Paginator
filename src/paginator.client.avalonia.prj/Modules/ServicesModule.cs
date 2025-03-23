
using Autofac;
using Paginator.Client.Avalonia.Services;

namespace Paginator.Client.Avalonia.Modules;
public class ServicesModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{ 
		builder
			.RegisterType<MainInfo>()
			.AsSelf() 
			.SingleInstance();

		builder
			.RegisterType<TestDataService>()
			.As<ITestDataService>()
			.AsSelf()
			.SingleInstance();

	}
}
