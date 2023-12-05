namespace Meowies.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Meowies!";
#pragma warning restore CA1822 // Mark members as static
}