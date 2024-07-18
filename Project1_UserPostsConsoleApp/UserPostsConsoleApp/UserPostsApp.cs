using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserPostsConsoleApp.Controller;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Service;

namespace UserPostsConsoleApp;

public class UserPostsApp {
    public static void Main(string[] args) {
        // Account account;

        using (var context = new ApplicationDbContext()) 
        {
            AccountDAO accountDAO = new AccountDAO(context);
            PostsDAO postsDAO = new PostsDAO(context);

            AccountService userService = new AccountService(accountDAO);
            PostsService postsService = new PostsService(postsDAO);

            ConsoleController consoleController = new ConsoleController(userService, postsService);

            consoleController.StartApp();


            // var createdUser = userService.createUser("Lio", "122333");

            // if (createdUser) 
            // {
            //     Console.WriteLine($"User succesfully created");
            // }

            // var loggedUser = userService.UserLogin("Lio", "122333");

            // if (loggedUser) 
            // {
            //     account = userService.GetAccount("Lio")!;

            //     if (account != null) 
            //     {
            //         Console.WriteLine("User successfully logged in");

            //         var post = postsService.createPost(account, "The rise of the Sun", "This is the story of a sun that arose");
            //         var post2 = postsService.createPost(account, "The downfall of the Sun", "This is the story of a sun that fell and die");
            //         var post3 = postsService.createPost(account, "The relection day", "This is the story of a day that was elected");

            //         Console.WriteLine(post);
            //     } 

            //     Console.WriteLine("All posts in db");
            //     foreach(var post in postsService.SeeAllPosts()!) 
            //     {
            //         Console.WriteLine(post);
            //     }

            //     Console.WriteLine("\n");

            //     Console.WriteLine($"From user Lio");
            //     foreach(var post in postsService.SeeAllUserPosts(account!)!) 
            //     {
            //         Console.WriteLine(post);
            //     }
            // }


        }

    }
}

