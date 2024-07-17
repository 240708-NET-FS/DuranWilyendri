using System;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Service;

namespace UserPostsConsoleApp.Controller;

public class ConsoleController 
{

    AccountService accountService;
    PostsService postsService;

    public ConsoleController (AccountService accountService, PostsService postsService) 
    {
        this.accountService = accountService;
        this.postsService = postsService;
    }

    public void menu() 
    {
        Console.WriteLine("Hello Poster, welcome to UserPosts App. Press 1 to Login or 2 to create an account...");
    
        string option = Console.ReadLine()!;
        int validOption = ValidateInputOptions(option);

        if (validOption != -1) 
        {
            if (validOption >= 1 && validOption <= 2) 
            {
                if (validOption == 1) 
                {
                    // promptUser to login
                } else 
                {
                    // promptUser to create account
                }
            } else 
            {
                Console.WriteLine("Invalid number. Please type either 1 or 2.");
            }

        } else 
        {
            Console.WriteLine("Invalid input. Please type either 1 or 2.");
        }

        
        
    }
    public void LoginSystem() 
    {
        Console.WriteLine("Please, type a username: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please, type a password: ");
        string passw = Console.ReadLine()!;


        if (!accountService.UserLogin(username, passw))
        {
            Console.WriteLine("Invalid credentials. Please, create an account you haven't done so.");
        } else 
        {
            // post menu
        }

    }

    public void PostingSystem(string username) 
    {
        Console.WriteLine($"Welcome back {username}. What would you like to do?");
        Console.WriteLine("Please, type the corresponding number according to the following options: ");
        Console.WriteLine("1. Post A Curiosity.\n 2. View My Posts.\n 3. View All Posts.");
        
        // string option = 



    }

    private int ValidateInputOptions(string toParse) 
    {
        if (int.TryParse(toParse, out int result)) 
        {
            return result;
        }

        return -1;
    }
}