using System.Configuration;
using System.Data;
using System.Windows;
using EntityFrameWorkHomeWork01.DbContext;
using EntityFrameWorkHomeWork01.Interfaces;
using EntityFrameWorkHomeWork01.Services;
using EntityFrameWorkHomeWork01.View;
using EntityFrameWorkHomeWork01.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MainPageViewModel = EntityFrameWorkHomeWork01.ViewModel.MainPageViewModel;

namespace EntityFrameWorkHomeWork01;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static ServiceCollection Collection { get; set; } = null!;

    public static ServiceProvider Provider { get; set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Collection = new ServiceCollection();

        Collection.AddSingleton<MainView>();
        Collection.AddSingleton<MainViewModel>();
        Collection.AddSingleton<MainPageViewModel>();
        Collection.AddSingleton<ApplicationDbContext>();
        Collection.AddSingleton<ConfigurationBuilder>();
        Collection.AddSingleton<IMessageBox, MyMessageBox>();

        Provider = Collection.BuildServiceProvider();

        var view = Provider.GetService<MainView>()!;

        view.ShowDialog();
    }
}