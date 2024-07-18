using UserPostsConsoleApp.Entities;
using Microsoft.IdentityModel.Tokens;
using UserPostsConsoleApp.DAO;
using System;
using UserPostsConsoleApp.Utility;
using Microsoft.Data.SqlClient;

namespace UserPostsConsoleApp.Service;

public class AccountService
{
    private AccountDAO accountDAO;

    public AccountService(AccountDAO accountDAO)
    {
        this.accountDAO = accountDAO;
    }

    public void CreateUser(string username, string password)
    {

        if (UsernameExists(username))
        {
            throw new CreateUserException("Username already taken. Try with a different one.");
        }

        var newAccount = new Account { Username = username, Password = password };

        accountDAO.Create(newAccount);
    }

    public Account UserLogin(string username, string password)
    {
        if (username.Length == 0 || password.Length == 0)
        {
            throw new InvalidInputException("Invalid credentials");
        }

        var account = accountDAO.GetByUsernameAndPassword(username, password);

        if (account != null)
        {

            return account;
        }

        throw new InvalidInputException("Invalid credentials.");


    }

    public bool UsernameExists(string username)
    {

        var account = accountDAO.GetByUsername(username);

        return account != null;
    }

    // public Account? GetAccount(string username)
    // {
    //     return UsernameExists(username) ? accountDAO.GetByUsername(username) : throw new Exception();
    // }


}