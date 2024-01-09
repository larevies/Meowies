using Avalonia.Controls;
using Meowies.ViewModels;

namespace Meowies.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }
}