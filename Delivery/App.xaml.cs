using Prism.Ioc;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows;
using Delivery.DAL.Repositories;
using Delivery.Services;
using Delivery.Views;
using AutoMapper;
using Delivery.ViewModels;

namespace Delivery
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<ISerializerService, XMLSerializerService>();
            containerRegistry.RegisterScoped<RunProcessService>();

            containerRegistry.RegisterScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
            containerRegistry.RegisterScoped<IDeliveryLogRepository, DeliveryLogRepository>();

            containerRegistry.RegisterDialog<AboutWindow, AboutViewModel>();
            containerRegistry.RegisterDialog<SettingsWindow, SettingsViewModel>();

            containerRegistry.RegisterScoped<IMapper>(_ => new MapperConfiguration(cfg => cfg.AddProfile<Profiler>()).CreateMapper());

            containerRegistry.RegisterScoped<IDbConnection>(_ =>
            {
                try
                {
                    var connection = new SQLiteConnection(@"Data Source=delivery.db");

                    connection.Open();

                    SQLiteConnection.Changed += (sender, eventArgs) =>
                        Debug.WriteLine($"{eventArgs.EventType}: {eventArgs.Text}");

                    return connection;
                }
                catch (SQLiteException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
