namespace UserPostsConsoleApp.Entities;

public class Posts 
{
    public int PostID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PostDate { get; set; }
    public int AccountID { get; set; }
    public Account Account { get; set; }

    public override string ToString()
    {
        return $"{Title}\n {Content}\n   - Posted By: {Account.Username} On {PostDate}";
    }
}