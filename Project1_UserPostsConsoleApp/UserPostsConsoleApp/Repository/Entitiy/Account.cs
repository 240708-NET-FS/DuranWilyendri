namespace UserPostsConsoleApp.Entities;

public class Account 
{
    public int AccountID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<Posts> Posts { get; set; }

    public override string ToString()
    {
        return $"{AccountID} {Username} {Password} ";
    }
}