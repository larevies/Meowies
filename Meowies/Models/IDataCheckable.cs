using System.Data.Common;
using System.Text.RegularExpressions;

namespace Meowies.Models;

public interface IDataCheckable
{
    public bool IsValid(string data);
}

public class NameChecker : IDataCheckable
{
    public bool IsValid(string name)
    {
        return !string.IsNullOrEmpty(name);
    }
}

public class EmailChecker : IDataCheckable
{
    public bool IsValid(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
}

public class PasswordChecker : IDataCheckable
{
    public bool IsValid(string password)
    {
        return !string.IsNullOrEmpty(password) && password.Length >= 8;
    }
}