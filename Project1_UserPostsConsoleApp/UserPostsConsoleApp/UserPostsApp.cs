using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Service;

namespace UserPostsConsoleApp;

public class UserPostsApp {
    public static void Main(string[] args) {
        Account account;

        using (var context = new ApplicationDbContext()) 
        {
            UserService userService = new UserService(context);
            PostsService postsService = new PostsService(context);


            var createdUser = userService.createUser("Lio", "122333");

            if (createdUser) 
            {
                Console.WriteLine($"User succesfully created");
            }

            var loggedUser = userService.userLogin("Lio", "122333");

            if (loggedUser) 
            {
                account = userService.GetAccount("Lio")!;

                if (account != null) 
                {
                    Console.WriteLine("User successfully logged in");

                    var post = postsService.createPost(account, "The rise of the Sun", "This is the story of a sun that arose", DateTime.Now);
                    var post2 = postsService.createPost(account, "The downfall of the Sun", "This is the story of a sun that fell and die", DateTime.Now);
                    var post3 = postsService.createPost(account, "The relection day", "This is the story of a day that was elected", DateTime.Now);

                    Console.WriteLine(post);
                } 

                Console.WriteLine("All posts in db");
                foreach(var post in postsService.seeAllPosts()!) 
                {
                    Console.WriteLine(post);
                }

                Console.WriteLine("\n");

                Console.WriteLine($"From user Lio");
                foreach(var post in postsService.seeAllUserPosts(account!)!) 
                {
                    Console.WriteLine(post);
                }
            }


        }

        using (var context = new ApplicationDbContext()) 
        {
            

        }

    }
}

