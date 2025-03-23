using Autofac;

using Paginator.Client.Avalonia.Views; 

namespace Paginator.Client.Avalonia.Modules;

public class ViewModelsModule : Autofac.Module
{ 
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<MainWindowView>() 
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<MainWindowViewModel>() 
			.AsSelf()
			.SingleInstance(); 

	}
}
