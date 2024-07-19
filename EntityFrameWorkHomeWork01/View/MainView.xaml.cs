using System.Windows;
using EntityFrameWorkHomeWork01.ViewModel;

namespace EntityFrameWorkHomeWork01.View;

public partial class MainView : Window
{
    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();

        DataContext = mainViewModel;
    }
}