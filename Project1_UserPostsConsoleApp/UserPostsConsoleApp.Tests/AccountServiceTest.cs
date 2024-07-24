using UserPostsConsoleApp.Service;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Utility;
using Microsoft.EntityFrameworkCore;

namespace UserPostsConsoleApp.Tests;

public class AccountServiceTest
{
    [Fact]
    public void CreateUser_ShouldReturnMessageWhenEmpty()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);

            Assert.Throws<CreateUserException>(() => accountService.CreateUser("", ""));
            Assert.Throws<CreateUserException>(() => accountService.CreateUser(null!, null!));

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
        }
    }

    [Fact]
    public void CreateUser_ShouldReturnMessageWhenUsernameTaken()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);
            accountService.CreateUser("existing user", "123");
            
            Assert.Throws<CreateUserException>(() => accountService.CreateUser("existing user", "1111"));
            
            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
    
        }
    }

    [Theory]
    [InlineData("existing user", "123")]
    [InlineData("existing user2", "231")]
    [InlineData("existing user3", "231")]
    public void CreateUser_ShouldCreateUserSuccesfully(string name, string pssw)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);
            accountService.CreateUser(name, pssw);

            var createdAccount = accountService.UsernameExists(name);

            Assert.True(createdAccount);

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
    
        }
    }

    [Fact]
    public void UserLogin_ShouldReturnInvalidInputExceptionWhenWrongCredentialsGiven()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);

            Assert.Throws<InvalidInputException>(() => accountService.UserLogin("user", "123"));
            
            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
    
        }
    }

      [Fact]
    public void UserLogin_ShouldReturnInvalidInputExceptionWhenEmpty()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);

            Assert.Throws<InvalidInputException>(() => accountService.UserLogin("", ""));

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
        }
    }

    [Theory]
    [InlineData("existing user", "123")]
    [InlineData("existing user2", "231")]
    [InlineData("existing user3", "231")]
    public void UserLogin_ShouldSuccefuslyLoggin(string name, string pssw)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);
            accountService.CreateUser(name, pssw);

            Assert.NotNull(accountService.UserLogin(name, pssw));

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
        }
    }

    [Fact]
    public void ChangePassword_ShouldReturnInvalidInputExceptionWhenEmpty()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);

            Assert.Throws<InvalidInputException>(() => accountService.ChangePassword("", "", ""));

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
        }
    }

    
    [Theory]
    [InlineData("existing user", "123", "321")]
    [InlineData("existing user2", "231", "123")]
    [InlineData("existing user3", "231", "132")]
    public void ChangePassword_ShouldSuccefuslyChangePassword(string name, string oldPassword, string newPassword)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);
            
            accountService.CreateUser(name, oldPassword);
            accountService.ChangePassword(name, oldPassword, newPassword);

            Assert.Equal(newPassword, accountService.UserLogin(name, newPassword).Password);

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
        }
    }

    [Theory]
    [InlineData("existing user", "123")]
    [InlineData("existing user2", "231")]
    [InlineData("existing user3", "231")]
    public void DeleteAccount_ShouldSuccesfullyDeleteUser(string username, string password)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            AccountService accountService = new AccountService(accountDAO);
            
            accountService.CreateUser(username, password);
            accountService.DeleteAccount(username, password);

            Assert.False(accountService.UsernameExists(username));

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.SaveChanges();
        }
    }

    [Fact]
    public void ValidateInput_ShouldReturnErrorMessageWhenLetterProvided()
    {
    
        Assert.Throws<InvalidInputException>(() => Validation.ValidateInputOptions("k"));
    }

    [Fact]
    public void ValidateInput_ShouldReturnIntegerWhenPassedAsString()
    {
    
        Assert.Equal(4, Validation.ValidateInputOptions("4"));
    }
}