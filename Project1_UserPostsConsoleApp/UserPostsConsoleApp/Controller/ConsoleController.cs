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
        Console.WriteLine("\nHello Poster, welcome to UserPosts App. Please, type one of the following: ");
        Console.WriteLine("1. Login.\n2. Create an Account.\n3. Change Password.\n4. Delete My Account.\nAny other key will close the application.");

        string option = Console.ReadLine()!;

        try
        {
            int validOption = Validation.ValidateInputOptions(option);


            if (validOption == 1)
            {
                LoginSystem();
            }
            else if (validOption == 2)
            {
                CreateAccountSystem();

            } else if (validOption == 3)
            {
                ChangePasswordSystem();

            } else if (validOption == 4)
            {
                DeleteAccountSystem();

            } 
            else
            {
                State.isActiveLogin = false;
            }
        }
        catch (InvalidInputException)
        {
            Console.WriteLine("\nSee you later.");
            State.isActiveLogin = false;
        }
    }

    public void LoginSystem()
    {
        Console.WriteLine("\nPlease, type your username: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please, type your password: ");
        string passw = Console.ReadLine()!;

        try
        {
            Account account = accountService.UserLogin(username, passw);
            State.isActiveSession = true;
            PostingSystem(account);

        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void PostingSystem(Account account)
    {
        Console.WriteLine($"\nWelcome back {account.Username}. What would you like to do?");

        while (State.isActiveSession)
        {
            Console.WriteLine("Please, type the corresponding number according to the following options: ");
            Console.WriteLine("1. Post A Curiosity.\n2. View My Posts.\n3. View All Posts.\n4. Logout.");

            string option = Console.ReadLine()!;

            try
            {
                int validOption = Validation.ValidateInputOptions(option);

                switch (validOption)
                {
                    case 1:
                        Console.WriteLine("\nWhat is the title of your post?");
                        string title = Console.ReadLine()!;

                        Console.WriteLine("Speak your mind...");
                        string content = Console.ReadLine()!;

                        Console.WriteLine("Posting...\n");
                        string post = postsService.CreatePost(account, title, content, DateTime.Now);

                        Console.WriteLine(post + "\n");
                        break;

                    case 2:
                        Console.WriteLine("\nEnjoy reading yourself...\n");
                        List<string> postedByUser = postsService.SeeAllUserPosts(account)!;

                        if (postedByUser == null || postedByUser.Count == 0)
                        {
                            Console.WriteLine("Oh oh, you have not posted anything yet.\n");
                        }
                        else
                        {
                            foreach (var posted in postedByUser!)
                            {
                                Console.WriteLine(posted + "\n");
                            }
                        }
                        break;

                    case 3:
                        Console.WriteLine("\nHere is what others have shared: \n");
                        List<string> allPosts = postsService.SeeAllPosts()!;

                        if (allPosts == null || allPosts.Count() == 0)
                        {
                            Console.WriteLine("Oh oh, there are no posts yet.\n");
                        }
                        else
                        {
                            foreach (var aPost in allPosts!)
                            {
                                Console.WriteLine(aPost + "\n");
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
                Console.WriteLine("Please type either 1, 2, or 3.\n");
            }
        }

    }

    private void CreateAccountSystem()
    {
        Console.WriteLine("\nPlease indicate your username: ");
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

    private void ChangePasswordSystem()
    {
        Console.WriteLine("\nPlease indicate your username: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please indicate current password: ");
        string oldPassword = Console.ReadLine()!;

        Console.WriteLine("Please indicate the new password: ");
        string newPassword = Console.ReadLine()!;

        try
        {
            accountService.ChangePassword(username, oldPassword, newPassword);
            Console.WriteLine("\nPassword succesfully changed.\n");

        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void DeleteAccountSystem() 
    {
        Console.WriteLine("\nPlease indicate your username: ");
        string username = Console.ReadLine()!;

        Console.WriteLine("Please indicate yout password: ");
        string password = Console.ReadLine()!;

        try
        {
            accountService.DeleteAccount(username, password);

            Console.WriteLine("\nAccount succesfully deleted.\n");

        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}