
using UserPostsConsoleApp.Entities;
using Moq;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Service;
using Microsoft.EntityFrameworkCore;

namespace UserPostsConsoleApp.Tests;

public class PostServiceTest
{
    [Fact]
    public void CreatePost_ShouldSuccesfullyCreatePost()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            // Setup
            PostsDAO postsDAO = new PostsDAO(context);
            PostsService postsService = new PostsService(postsDAO);

            // Arrange
            string title = "title_test";
            string content = "content_test";
            Account account = new Account{AccountID = 1, Username = "user_test", Password = "psww"};
            context.Accounts.Add(account);
            context.SaveChanges();

            DateTime postDate = DateTime.Now;
            
            // Act
            string post = postsService.CreatePost(account, title, content, postDate);
            
            string expected = $"{title}\n {content}\n   - Posted By: {account.Username} On {postDate}";

            // Assert
            Assert.Equal(expected, post);

            // Clean DB to ensure clean state for next run
            context.Accounts.RemoveRange(context.Accounts);
            context.Posts.RemoveRange(context.Posts);
        }
    }

    
    [Fact]
    public void SeeAllUserPosts_ShouldSuccesfullyReturnAllPosts()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AMyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            // Setup
            PostsDAO postsDAO = new PostsDAO(context);
            PostsService postsService = new PostsService(postsDAO);
           
            // Arrange
            Account account = new Account{AccountID = 1, Username = "user_test1", Password = "psww"};

            string title = "title_test1";
            string content = "content_test1";
            string title2 = "title_test2";
            string content2 = "content_test2";
            DateTime postDate = new DateTime(2024, 6, 7);
            DateTime postDate2 = new DateTime(2026, 11, 12);

            context.Accounts.Add(account);
            context.SaveChanges();
            
            Posts post1 = new Posts{PostID = 1, Title = title, Content = content, AccountID = 1, Account = account, PostDate = postDate};
            postsService.CreatePost(account, title, content, postDate);

            Posts post2 = new Posts{PostID = 2, Title = title2, Content = content2, AccountID = 2, Account = account, PostDate = postDate2};
            postsService.CreatePost(account, title2, content2, postDate2);

            List<string> expectedList = new List<string>();
            expectedList.Add(post1.ToString());
            expectedList.Add(post2.ToString());
            
            // Act
            List<string>? posts = postsService.SeeAllPosts();

            // Assert
            Assert.Equal(expectedList, posts);

            // Clean DB to ensure clean state for next run
            context.Posts.RemoveRange(context.Posts);
            context.Accounts.RemoveRange(context.Accounts);
        }
    }

    [Fact]
    public void SeeAllPosts_ShouldSuccesfullyReturnAllPosts()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("BMyDb").Options;
        
        using (var context = new ApplicationDbContext(options)) 
        {
            // Setup
            PostsDAO postsDAO = new PostsDAO(context);
            PostsService postsService = new PostsService(postsDAO);
           
            // Arrange
            Account account = new Account{AccountID = 1, Username = "user_test1", Password = "psww"};
            Account account2 = new Account{AccountID = 2, Username = "user_test2", Password = "psww"};

            string title = "title_test1";
            string content = "content_test1";
            string title2 = "title_test2";
            string content2 = "content_test2";
            DateTime postDate = new DateTime(2024, 6, 7);
            DateTime postDate2 = new DateTime(2026, 11, 12);

            context.Accounts.Add(account);
            context.Accounts.Add(account2);
            context.SaveChanges();
            
            Posts post1 = new Posts{PostID = 1, Title = title, Content = content, AccountID = 1, Account = account, PostDate = postDate};
            postsService.CreatePost(account, title, content, postDate);

            Posts post2 = new Posts{PostID = 2, Title = title2, Content = content2, AccountID = 2, Account = account2, PostDate = postDate2};
            postsService.CreatePost(account2, title2, content2, postDate2);

            List<string> expectedList = new List<string>();
            expectedList.Add(post1.ToString());
            expectedList.Add(post2.ToString());
            
            // Act
            List<string>? posts = postsService.SeeAllPosts();

            // Assert
            Assert.Equal(expectedList, posts);

            // Clean DB to ensure clean state for next run
            context.Posts.RemoveRange(context.Posts);
            context.Accounts.RemoveRange(context.Accounts);
        }
    }
}