using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace Meowies.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        _currentPage = _pages[0];

        var canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
        var canNavPrev = this.WhenAnyValue(x => x.CurrentPage.CanNavigatePrevious);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
    }
    
    private string _next = "Sign up";
    private string _previous = "Sign in";
    public string Next
    {
        get => _next;
        set => this.RaiseAndSetIfChanged(ref _next, value);
    }
    public string Previous
    {
        get => _previous;
        set => this.RaiseAndSetIfChanged(ref _previous, value);
    }
    
    private readonly PageViewModelBase[] _pages = 
    { 
        new WelcomeViewModel(),
        new SignUpViewModel(),
        new SignInViewModel()
    };
    
    private PageViewModelBase _currentPage;
    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    public ICommand NavigateNextCommand { get; }

    private void NavigateNext()
    {
        var index = _pages.IndexOf(CurrentPage) + 1;
        CurrentPage = _pages[index];
        if (CurrentPage == _pages[2])
        {
            Next = "Sign in";
            Previous = "Go back";
        }
        else if (CurrentPage == _pages[0])
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentPage == _pages[1])
        {
            Next = "Sign up";
            Previous = "Go Back";
        }
    }
    public ICommand NavigatePreviousCommand { get; }
    private void NavigatePrevious()
    {
        int index;
        if (CurrentPage == _pages[0])
        {
            index = _pages.IndexOf(CurrentPage) + 2;
        }
        else
        {
            index = 0;
        }
        CurrentPage = _pages[index];
        if (CurrentPage == _pages[2])
        {
            Next = "Sign in";
            Previous = "Go back";
        }
        else if (CurrentPage == _pages[0])
        {
            Next = "Sign up";
            Previous = "Sign in";
        }
        else if (CurrentPage == _pages[1])
        {
            Next = "Sign up";
            Previous = "Go Back";
        }
    }
}