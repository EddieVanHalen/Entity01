using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EntityFrameWorkHomeWork01.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameWorkHomeWork01.ViewModel;

[INotifyPropertyChanged]
public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty] private BaseViewModel _currentViewModel;

    public MainViewModel()
    {
        WeakReferenceMessenger.Default.Register<ChangeViewModelMessage>(this, (sender, message) =>
        {
            CurrentViewModel = message.ViewModel;
        });

        var model = App.Provider.GetService<MainPageViewModel>()!;
        var message = new ChangeViewModelMessage(model);
        WeakReferenceMessenger.Default.Send(message);
    }
}