namespace Meowies.Models;

using System.Linq;

public interface IDbUpdatable
{
    public void Update(ulong userId, string T);
}

public class EmailUpdater : IDbUpdatable
{
    public void Update(ulong userId, string newEmail)
    {
        //MeowiesContext meowCtx = new MeowiesContext();
        //User user = meowCtx.Users.Single(x => x.Id == userId);
        // TODO: check if email is correct and not the same as before
        // if (user.Email == newEmail)
        // {
        //     throw new Exception($"Введённая почта совпадает с актуальной почтой");
        // }
        // if (newEmail.isIncorrect())
        // {
        //     throw new Exception("Введённая почта некорректна");
        // }
        //user.Email = newEmail;
        //meowCtx.SaveChanges();
    }
}

public class PasswordUpdater : IDbUpdatable
{
    public void Update(ulong userId, string newPassword)
    {
        MeowiesContext meowCtx = new MeowiesContext();
        User user = meowCtx.Users.Single(x => x.Id == userId);
        // TODO: check if password is correct and not the same as before
        // if (user.Password == newPassword)
        // {
        //     throw new Exception($"Введённый пароль совпадает с предыдущим");
        // }
        // if (newPassword.isIncorrect())
        // {
        //     throw new Exception("Введённый пароль не соответствует стандартам безопасности");
        // }
        user.Password = newPassword;
        meowCtx.SaveChanges();
    }
}

public class NameUpdater : IDbUpdatable
{
    public void Update(ulong userId, string newName)
    {
            //MeowiesContext meowCtx = new MeowiesContext();
            //User user = meowCtx.Users.Single(x => x.Id == userId);
            //user.Name = newName;
            //meowCtx.SaveChanges();
    }
}