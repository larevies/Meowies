using System;
using System.Windows.Input;
using Meowies.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Meowies.Commands;

public class UpdateViewCommand : ICommand
{
    private MainWindowViewModel _viewModel;

    public UpdateViewCommand(MainWindowViewModel viewModel)
    {
        _viewModel = viewModel;
    }
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter == null) throw new ArgumentNullException(nameof(parameter));
        switch (parameter.ToString())
        {
            case "Search": _viewModel.SelectedViewModel = new SearchViewModel();
                break;
            case "Bookmarks": _viewModel.SelectedViewModel = new SearchViewModel();
                break;
            case "Profile": _viewModel.SelectedViewModel = new ProfileViewModel();
                break;
        }
    }

    public event EventHandler? CanExecuteChanged;
}