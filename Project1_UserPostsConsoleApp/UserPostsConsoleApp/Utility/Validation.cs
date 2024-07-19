namespace UserPostsConsoleApp.Utility;

public static class Validation 
{
     public static int ValidateInputOptions(string toParse)
    {
        if (int.TryParse(toParse, out int result))
        {
            return result;
        }

        throw new InvalidInputException("Inavlid input");
    }
}