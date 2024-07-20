using UserPostsConsoleApp;
using Xunit;
using System;
using System.IO;
using UserPostsConsoleApp.Service;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Utility;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace UserPostsConsoleApp.Tests;

public class UserPostsConsoleAppTest
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