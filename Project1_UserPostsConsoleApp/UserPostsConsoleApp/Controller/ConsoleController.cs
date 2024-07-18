using System;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Service;
using UserPostsConsoleApp.Utility;

namespace UserPostsConsoleApp.Controller;

public class ConsoleController
{

    AccountService accountService;
    PostsService postsService;

    public ConsoleController(AccountService accountService, PostsService postsService)
    {
        this.accountService = accountService;
        this.postsService = postsService;
    }

    public void StartApp()
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
                    LoginSystem();
                }
                else
                {
                    CreateAccountSystem();
                }
            }
            else
            {
                Console.WriteLine("Invalid number. Please type either 1 or 2.");
            }

        }
        else
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

        try
        {
            Account account = accountService.UserLogin(username, passw);
            PostingSystem(account);

        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine(ex.Message);
        }



    }

    public void PostingSystem(Account account)
    {
        Console.WriteLine($"Welcome back {account.Username}. What would you like to do?");
        Console.WriteLine("Please, type the corresponding number according to the following options: ");
        Console.WriteLine("1. Post A Curiosity.\n 2. View My Posts.\n 3. View All Posts.\n 4. Logout");

        string option = Console.ReadLine()!;

        try
        {
            int validOption = ValidateInputOptions(option);

            switch (validOption)
            {
                case 1:
                    Console.WriteLine("What is the title of your post?");
                    string title = Console.ReadLine()!;

                    Console.WriteLine("Speak your mind...");
                    string content = Console.ReadLine()!;

                    Console.WriteLine("Posting...");
                    string post = postsService.CreatePost(account, title, content);

                    Console.WriteLine(post);
                    break;

                case 2:
                    Console.WriteLine("Enjoy reading yourself...");
                    List<string> postedByUser = postsService.SeeAllUserPosts(account)!;

                    if (postedByUser == null)
                    {
                        Console.WriteLine("Oh oh, you have not posted anything yet.");
                    }
                    else
                    {
                        foreach (var posted in postedByUser!)
                        {
                            Console.WriteLine(posted);
                        }
                    }
                    break;

                case 3:
                    Console.WriteLine("Here is what others have shared: ");
                    List<string> allPosts = postsService.SeeAllPosts()!;

                    if (allPosts == null)
                    {
                        Console.WriteLine("Oh oh, there are no posts yet.");
                    }
                    else
                    {
                        foreach (var aPost in allPosts!)
                        {
                            Console.WriteLine(aPost);
                        }
                    }
                    break;
                case 4:
                    Console.WriteLine($"See you later {account.Username}.");
                    break;
            }
        }
        catch (InvalidInputException)
        {
            Console.WriteLine("Please type either 1, 2, or 3.");
        }
    }

    private void CreateAccountSystem()
    {
        Console.WriteLine("Please indicate your username: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please indicate your password: ");
        string password = Console.ReadLine()!;

        try
        {
            accountService.CreateUser(username, password);

        }
        catch (CreateUserException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private int ValidateInputOptions(string toParse)
    {
        if (int.TryParse(toParse, out int result))
        {
            return result;
        }

        throw new InvalidInputException("Inavlid input");
    }
}