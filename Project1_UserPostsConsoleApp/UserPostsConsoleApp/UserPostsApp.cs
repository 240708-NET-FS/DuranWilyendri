using System;
using Microsoft.EntityFrameworkCore;
using UserPostsConsoleApp.Entities;

namespace UserPostsConsoleApp;

public class UserPostsApp {
    public static void Main(string[] args) {

        using (var context = new ApplicationDbContext())
        {
            var user = new Account {Username = "Wil", Password = "1234"};

            context.Accounts.Add(user);
            context.SaveChanges();
           
        }

        using (var context = new ApplicationDbContext())
        {
           var account = context.Accounts
                .FirstOrDefault(e => e.Username == "Wil");

            Console.WriteLine(account);
        }
    }
}

