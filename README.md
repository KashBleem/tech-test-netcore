# Storyteller Technical Test – A Todo-List Project

This is a todo-list webapp written in C# using [.NET 8.0](https://get.asp.net).

Show us what you've got by knocking it into shape!

## Practicalities

Please make a fork of this project for your work.

Each commit you make should relate to a single task. A more complex task may have many commits; this is up to you.

The app runs on Windows, macOS, and Linux. It can be built using Visual Studio, Visual Studio Code, Rider, or the command line. On the command line: 

| What | How |
|-|-|
| build | `dotnet build` |
| run unit tests | `dotnet test Todo.Tests` |
| run | `dotnet run --project Todo` |

If you run the tests, you should see that one fails. This is deliberate.

## SQLite Setup

This project uses SQLite for its database. Follow these instructions to set up and configure SQLite:

1. Ensure you have the necessary tools installed:
    - [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
    - [SQLite](https://www.sqlite.org/download.html)

2. Configure the SQLite database:
    - Open a terminal or command prompt in the root of the project directory.
    - Run the following command to apply the latest Entity Framework migrations and create the database:
      ```bash
      dotnet ef database update
      ```

3. Verify that the SQLite database has been created:
    - You should see a new file named `Todo.db` in the project directory. This file represents your SQLite database.

4. Running the application:
    - Use the following command to run the application:
      ```bash
      dotnet run --project Todo
      ```

5. (Optional) Explore the database:
    - You can use tools like [DB Browser for SQLite](https://sqlitebrowser.org/) to open and inspect the `Todo.db` file.

## The app

The app allows a user to create multiple todo-lists. Each list has a number of items. Each item has an importance and a person responsible for completion.

## Tasks

Tasks are in ascending order of difficulty. Complete as many, or as few, tasks that you feel able to. Add unit tests if you can.

> **Hey this is important!**
> We hope you can spend between 3 and 5 hours on this project. If you can finish faster -- great! If not, limit yourself and don't spend much longer than 5 hours MAX.

If you want to document any aspects of your solution, please use `SOLUTION.md` in the root of the repository.

| # | Description |
|-|-|
| 1 | Build and run the app. Register a user account, make some lists, add some items – have a play and get familiar with the app. |
| 2 | When todo items are displayed in browser in the details page, they are listed in an arbitrary order. Change `Views/TodoList/Detail.cshtml` so that items are listed by order of importance: `High`, `Medium`, `Low` |
| 3 | Run the unit tests. One test should be failing. The process that maps a `TodoItem` to a `TodoItemEditFields` instance is failing - the `Importance` field is not copied. Fix the bug and ensure the test passes. |
| 4 | Make it so that the edit and create item pages show friendly text instead of "ResponsiblePartyId"  |
| 5 | On the details page, add an option to hide items that are marked as done. |
| 6 | Currently `/TodoList` shows all todo-lists that the user is owner of. Change this so it also shows todo-lists that the user has at least one item where they are marked as the responsible party  |
| 7 | Add a `Rank` property to the `TodoItem` class. Add an EntityFramework migration to reflect this change. Allow a user to set the rank property on the edit page. Add a new option on the details page to order by rank. |
| 8 | If the users you register have an avatar added to gravatar.com, then you will see that avatar by their email address in the navigation area and beside items in a list. Instead of just showing an email address to identify a user, make an enhancement that uses the gravatar.com API to download profile information (if any), and extract the user's name. Display the name along side the email address. Consider what would happen if the gravatar service was slow to respond or not working. |
| 9 | The process of adding items to a list is pretty clunky; the user has to go to a new page, fill in a form, then go back to the list detail page. It would be easier for the user to do all that on the list detail page. Replace the "Add New Item" link with UI that allows creation of items without navigating away from the detail page. You will need to use Javascript and an API that you create. |
| 10 | Add an API that allows setting of the `Rank` property (added in Task 7). Add Javascript functionality that allows reordering of list items by rank without navigating away from the detail page |
