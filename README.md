# Banking Console App

## Requirements
- The application should be a C# Console Application
- The application should build and run
- The application should interact with users, and provide some console UI
- The application should demonstrate some input validation
- The application should have unit tests
- The application should persist data to a SQL Server database
- The application should communicate to DB via EF Core (Entity Framework Core)

## Application Description
The application will guide users through the process of either logging in or creating an account. User inputs are integers representing menu options or strings representing a message to post.

### Log In / Create Account
1. User selects an option to Log In (1) or Create Account (2).
2. If the user selects option 2, they will be prompted to enter a unique username and password.
3. If the user selects option 1, they will be welcomed to the application and presented with options: Post, See My Posts, See All Posts, and Log Out.
4. If the user does not exist, display a message indicating invalid credentials and repeat step 1.

### See My Posts
1. The application verifies that the user input is a positive integer that indicates the right choice.
2. All posts created by the current user are retrieved from DB and printed to the screen along with the date created.
4. The user is presented with options again: Post, See My Posts, See All Posts, and Log Out.

### See All Posts
1. The application verifies that the user input is a positive integer that indicates the right choice.
2. All posts created by all users are retrieved from DB and printed to the screen along with the date created.
4. The user is presented with options again: Post, See My Posts, See All Posts, and Log Out.

### Log Out
1. The application verifies that the user input is a positive integer that indicates the right choice.
2. The user selects option 4 to exit the application.

### Approach
1. Implement necessary validations: password validation during login, integer validation for menu choice, and string validation for posts.
2. Develop and test methods for posting, login, and reading posts.
3. Perform multiple database read and write operations for posting and viewing posts.
4. Use EF Core (Entity Framework Core) to handle database interactions.

