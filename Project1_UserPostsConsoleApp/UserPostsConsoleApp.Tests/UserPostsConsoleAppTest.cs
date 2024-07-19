using UserPostsConsoleApp;
using Xunit;
using System;
using System.IO;
using UserPostsConsoleApp.Service;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Utility;
using Moq;

namespace UserPostsConsoleApp.Tests;

public class UserPostsConsoleAppTest
{
    // [Fact]
    // public void CreateUser_ShouldReturnMessageWhenEmpty()
    // {
    //     var context = new Mock<ApplicationDbContext>();
    //     var accountDAO = new Mock<AccountDAO>(context.Object);
    //     var accountService = new AccountService(accountDAO.Object);

    //     Assert.Throws<CreateUserException>(() => accountService.CreateUser("", ""));
    // }

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