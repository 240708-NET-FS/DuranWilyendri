using UserPostsConsoleApp.Entities;
using Microsoft.IdentityModel.Tokens;
using UserPostsConsoleApp.DAO;

namespace UserPostsConsoleApp.Service;

public class PostsService
{
    private PostsDAO postsDAO;

    public PostsService(PostsDAO postsDAO)
    {
        this.postsDAO = postsDAO;
    }

    public string createPost(Account account, string title, string content)
    {

        Posts post = new Posts { Title = title, Content = content, PostDate = DateTime.Now, AccountID = account.AccountID };

        postsDAO.Create(post);


        return post.ToString();
    }

    public List<string>? seeAllUserPosts(Account account)
    {

        List<string> postList = new List<string>();

        var posts = postsDAO.GetAllByAUser(account).ToList();

        if (posts != null)
        {
            for (int i = 0; i < posts.Count(); i++)
            {
                postList.Add(posts[i].ToString());

            }
        }


        return postList;


    }

    public List<string>? seeAllPosts()
    {

        List<string> postList = new List<string>();

        var posts = postsDAO.GetAll().ToList();

        if (posts != null)
        {
            for (int i = 0; i < posts.Count(); i++)
            {
                postList.Add(posts[i].ToString());
            }
        }


        return postList;

    }

}