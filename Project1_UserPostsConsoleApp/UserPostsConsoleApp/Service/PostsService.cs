using UserPostsConsoleApp.Entities;
using Microsoft.IdentityModel.Tokens;
using System;

namespace UserPostsConsoleApp.Service;

public class PostsService 
{
    private ApplicationDbContext applicationDbContext;
    
    public PostsService(ApplicationDbContext applicationDbContext) 
    {
        this.applicationDbContext = applicationDbContext;
    }

    public string createPost(Account account, string title, string content, DateTime date) 
    {
        try 
        {
            Posts post = new Posts{Title = title, Content = content, PostDate = date, AccountID = account.AccountID};
            
            applicationDbContext.Posts.Add(post);
            applicationDbContext.SaveChanges();


            return post.ToString();

        } catch (Exception ex) 
        {
            throw new Exception($"Error when crating post for user {account.Username}: {ex.Message}");
        }
    }

    public List<string>? seeAllUserPosts (Account account) 
    {
        try 
        {
            List<string> postList = new List<string>();
        
            var posts = applicationDbContext.Posts.Where(p => p.AccountID == account.AccountID).ToList();

            if (posts != null) 
            {
                for (int i = 0; i < posts.Count(); i++) {
                    postList.Add(posts[i].ToString());
                }
            }


            return postList;      

        } catch (Exception ex) 
        {
            throw new Exception($"Error when retrieving posts from user {account.Username}: {ex.Message}");
          
        }
    }

    public List<string>? seeAllPosts() 
    {
        try 
        {
            List<string> postList = new List<string>();
        
            var posts = applicationDbContext.Posts.ToList();

            if (posts != null) 
            {
                for (int i = 0; i < posts.Count(); i++) {
                    postList.Add(posts[i].ToString());
                }
            }


            return postList;      

        } catch (Exception ex) 
        {
            throw new Exception($"Error when retrieving all posts: {ex.Message}");
        }
    }

}