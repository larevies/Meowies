using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Meowies.Views;

public partial class BookmarksView : UserControl
{
    public BookmarksView()
    {
        InitializeComponent();
    }


    /*private void ListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var index = ListBox.SelectedIndex;
        if (ListBox.SelectedIndex >= 0)
        {
            ListBox.Items.RemoveAt(index);
        }
    }*/
}