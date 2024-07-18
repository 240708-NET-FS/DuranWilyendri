using Microsoft.EntityFrameworkCore;
using UserPostsConsoleApp.Entities;

namespace UserPostsConsoleApp.DAO;

public class PostsDAO : IDAO<Posts>
{
    ApplicationDbContext context;

    public PostsDAO(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Create(Posts item)
    {
        context.Posts.Add(item);
        context.SaveChanges();
    }

    public void Delete(Posts item)
    {
        context.Posts.Remove(item);
        context.SaveChanges();
    }

    public ICollection<Posts> GetAll()
    {
        return context.Posts.Include(p => p.Account).ToList();
    }

    public Posts GetById(int ID)
    {
        return context.Posts.FirstOrDefault(p => p.PostID == ID)!;
    }

    public void Update(Posts item)
    {

        Posts originalPosts = context.Posts.FirstOrDefault(p => p.PostID == item.PostID)!;

        if (originalPosts != null)
        {
            originalPosts.Title = item.Title;
            originalPosts.Content = item.Content;
            originalPosts.PostDate = item.PostDate;
        }

        context.Posts.Update(originalPosts!);
        context.SaveChanges();
    }

     public ICollection<Posts> GetAllByAUser(Account account)
    {
        return context.Posts.Where(p => p.AccountID == account.AccountID).ToList();
    }
}