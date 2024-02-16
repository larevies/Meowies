using System;
using System.ComponentModel;

namespace Meowies.Models;

public class User : INotifyPropertyChanged
{
    public ulong Id { get; set; }
    private string _name = null!;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Birthday { get; set; }
    public int ProfilePicture { get; set; }
    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}, Birthday: {Birthday}, Password: {Password})";
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}