# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

# STEP 1 — Fix your project FIRST
Run these commands in terminal:

dotnet remove package Microsoft.EntityFrameworkCore
dotnet remove package Pomelo.EntityFrameworkCore.MySql

# STEP 2 — Install correct aligned versions
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.25
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.25
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.25
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.2

# STEP 3 — Restore cleanly
dotnet restore

# STEP 4 — NOW run migration
dotnet ef migrations add InitialCreate
dotnet ef database update

# STEP 5 - Update Migration
dotnet ef database drop                         
dotnet ef database update