using System;
using System.ComponentModel;

namespace Meowies.Models;

public class User
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Birthday { get; set; }
    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}, Birthday: {Birthday}, Password: {Password})";
    }
}