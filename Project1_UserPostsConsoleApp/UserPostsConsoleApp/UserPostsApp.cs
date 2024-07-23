using UserPostsConsoleApp.Controller;
using UserPostsConsoleApp.DAO;
using UserPostsConsoleApp.Entities;
using UserPostsConsoleApp.Service;
using UserPostsConsoleApp.Utility;

namespace UserPostsConsoleApp;

public class UserPostsApp
{
    public static void Main(string[] args)
    {
        using (var context = new ApplicationDbContext())
        {
            AccountDAO accountDAO = new AccountDAO(context);
            PostsDAO postsDAO = new PostsDAO(context);

            AccountService userService = new AccountService(accountDAO);
            PostsService postsService = new PostsService(postsDAO);

            ConsoleController consoleController = new ConsoleController(userService, postsService);

            State.isActiveLogin = true;
            State.isActiveSession = true;

            while (State.isActiveLogin)
            {
                consoleController.StartApp();
            }

        }

    }
}

