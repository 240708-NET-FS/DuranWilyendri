using UserPostsConsoleApp.Entities;
using Microsoft.IdentityModel.Tokens;
using System;

namespace UserPostsConsoleApp.Service;

public class UserService 
{
    private ApplicationDbContext applicationDbContext;
    
    public UserService(ApplicationDbContext applicationDbContext) 
    {
        this.applicationDbContext = applicationDbContext;
    }

    public Boolean createUser(string userName, string password) 
    {
        try 
        {
            if(usernameExists(userName)) 
            {
                throw new  InvalidOperationException("Username already taken");
            }
            var newAccount = new Account{Username = userName, Password = password};

            applicationDbContext.Accounts.Add(newAccount);
            applicationDbContext.SaveChanges();

            return true;

        } catch (Exception ex) 
        {
            throw new Exception($"Error creating user {userName}: {ex.Message}");
        }
        
    }

    public Boolean userLogin(string userName, string password) 
    {
        try 
        {
            var account = applicationDbContext.Accounts.FirstOrDefault(e => e.Username == userName);

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

        } catch (Exception ex) 
        {
            throw new Exception($"Db error when logging in user {userName}: {ex.Message}");
        }
    }

    public Boolean usernameExists(string userName) 
    {
        try
        {
            var account = applicationDbContext.Accounts.FirstOrDefault(e => e.Username == userName);

            return account != null;
            
        } catch (Exception ex) 
        {
            throw new Exception($"Db error when verifying existance of user {userName}: {ex.Message}");
        }
        
    }

    public Account? GetAccount(string username)
    {
        return usernameExists(username) ? applicationDbContext.Accounts.FirstOrDefault(e => e.Username == username) : null;
    }


}