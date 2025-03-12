using Autofac;
using BIMAIAnalyzer.Civil3D.ViewModels;
using BIMAIAnalyzer.Civil3D.Views;
using Prism.Events;

namespace BIMAIAnalyzer.Civil3D.Models
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MainViewViewModel>().AsSelf();
            builder.RegisterType<MainView>().AsSelf();

            return builder.Build();
        }
    }
}
