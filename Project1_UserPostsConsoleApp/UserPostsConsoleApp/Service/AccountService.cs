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

    public bool CreateUser(string username, string password) 
    {
        try 
        {
            if(usernameExists(username)) 
            {
                throw new  CreateUserException("Username already taken");
            }

            var newAccount = new Account{Username = username, Password = password};

            accountDAO.Create(newAccount);

            return true;

        } catch (SqlException ex) 
        {
            throw new Exception($"Error creating user {username}: {ex.Message}");
        }
        
    }

    public bool UserLogin(string username, string password) 
    {
      
        var account = accountDAO.GetByUsername(username);

        if (account != null) 
        {
            if (account.Password == password) 
            {
                return true;

            } else 
            {
                return false;
            }
        }
        
        return false;

    
    }

    public bool usernameExists(string username) 
    {
        try
        {
            var account = accountDAO.GetByUsername(username);

            return account != null;
            
        } catch (SqlException ex) 
        {
            throw new Exception($"Db error when verifying existance of user {username}: {ex.Message}");
        }
        
    }

    public Account? GetAccount(string username)
    {
        return usernameExists(username) ?  accountDAO.GetByUsername(username) : throw new Exception();
    }


}