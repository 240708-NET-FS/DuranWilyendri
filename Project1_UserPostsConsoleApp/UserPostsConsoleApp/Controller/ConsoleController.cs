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
        Console.WriteLine("Hello Poster, welcome to UserPosts App. Please, type 1 to login, 2 to Create an Account, or any other key to leave the application.");

        string option = Console.ReadLine()!;

        try
        {
            int validOption = ValidateInputOptions(option);

            if (validOption >= 1 && validOption <= 2)
            {
                if (validOption == 1)
                {
                    LoginSystem();
                }
                else if (validOption == 2)
                {
                    CreateAccountSystem();
                }
            }
            else
            {
                State.isActiveLogin = false;
            }
        }
        catch (InvalidInputException)
        {
            State.isActiveLogin = false;
        }
    }
    
    public void LoginSystem()
    {
        Console.WriteLine("Please, type your username: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please, type your password: ");
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

        while (State.isActiveSession)
        {
            Console.WriteLine("Please, type the corresponding number according to the following options: ");
            Console.WriteLine("1. Post A Curiosity.\n2. View My Posts.\n3. View All Posts.\n4. Logout");

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

                        // TODO: not working null
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
                        State.isActiveSession = false;
                        break;
                }
            }
            catch (InvalidInputException)
            {
                Console.WriteLine("Please type either 1, 2, or 3.");
            }
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