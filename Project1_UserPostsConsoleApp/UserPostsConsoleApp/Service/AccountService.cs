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

        if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
        {
            throw new CreateUserException("Username and password are mandatory fields.");
        }

        var newAccount = new Account { Username = username, Password = password };

        accountDAO.Create(newAccount);
    }

    public Account UserLogin(string username, string password)
    {
        if (username.Length == 0 || password.Length == 0)
        {
            throw new InvalidInputException("Username and password are mandatory fields.");
        }

        var account = accountDAO.GetByUsernameAndPassword(username, password);

        if (account != null)
        {

            return account;
        }

        throw new InvalidInputException("Invalid credentials.");


    }

    public void ChangePassword(string username, string oldPassword, string newPassword) 
    {
        if (username.IsNullOrEmpty() || newPassword.IsNullOrEmpty()) 
        {
            throw new InvalidInputException("Username and password are mandatory fields.");
        }

        Account existingAccount = accountDAO.GetByUsernameAndPassword(username, oldPassword) ?? throw new InvalidInputException("Wrong username or password.");
        existingAccount.Password = newPassword!;
        accountDAO.Update(existingAccount);
    }

    public void DeleteAccount(string username, string password) 
    {
        if (username.IsNullOrEmpty() || password.IsNullOrEmpty()) 
        {
            throw new InvalidInputException("Username and password are mandatory fields.");
        }

        Account existingAccount = accountDAO.GetByUsernameAndPassword(username, password) ?? throw new InvalidInputException("Wrong username or password.");
        
        accountDAO.Delete(existingAccount);
    }

    public bool UsernameExists(string username)
    {

        var account = accountDAO.GetByUsername(username);

        return account != null;
    }

}