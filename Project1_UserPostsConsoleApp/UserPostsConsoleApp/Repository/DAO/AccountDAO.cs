using Microsoft.EntityFrameworkCore;
using UserPostsConsoleApp.Entities;

namespace UserPostsConsoleApp.DAO;


public class AccountDAO : IDAO<Account>
{
    ApplicationDbContext context;

    public AccountDAO(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Create(Account item)
    {
        context.Accounts.Add(item);
        context.SaveChanges();

    }

    public void Delete(Account item)
    {
        context.Accounts.Remove(item);
        context.SaveChanges();
    }

    public ICollection<Account> GetAll()
    {
        return context.Accounts.ToList();
    }

    public Account GetById(int ID)
    {
        return context.Accounts.FirstOrDefault(a => a.AccountID == ID)!;
    }

    public void Update(Account item)
    {
        Account original = context.Accounts.FirstOrDefault(a => a.AccountID == item.AccountID)!;

        if (original != null)
        {
            original.Username = item.Username;
            original.Password = item.Password;
        }


        context.Accounts.Update(original!);
        context.SaveChanges();
    }

    public Account GetByUsernameAndPassword(string username, string password)
    {
        return context.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password)!;
    }

    public Account GetByUsername(string username)
    {
        return context.Accounts.FirstOrDefault(a => a.Username == username)!;
    }
}