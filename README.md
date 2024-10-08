[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/9HM9TmKV)

![](http://images.restapi.co.za/pvt/Noroff-64.png)

# Noroff

## Back-end Development Year 2

### BET - Course Assignment

Classroom repository for Noroff back-end development 2 - BET Course Assignment.

Instruction for the course assignment is in the LMS (Moodle) system of Noroff.
[https://lms.noroff.no](https://lms.noroff.no)

![](http://images.restapi.co.za/pvt/important_icon.png)

You will not be able to make any submission after the deadline of the course assignment. Make sure to make all your commit **BEFORE** the deadline

![](http://images.restapi.co.za/pvt/help_small.png)

If you are unsure of any instructions for the course assignment, contact out to your teacher on **Microsoft Teams**.

**REMEMBER** Your Moodle LMS submission must have your repository link **AND** your Github username in the text file.

---

## Application setup instructions

```
// how I created the project
dotnet new webapi -controllers -f net8.0

// added the .gitignore file
dotnet new gitignore

// installed EF cli tool
dotnet tool install --global dotnet-ef

// installed the packages
dotnet add package MySQL.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
```

## Instructions to run the application

```
dotnet run
```

and then open your browser to http://localhost:PORT/swagger/index.html
replace PORT with the port is showing on terminal after executing dotnet

## Instructions to create needed Migrations

```
// create migration
dotnet ef migrations add -c DataContext Final

// update the Database and create the tables,fields and relationships
dotnet ef database update
```

## Connection String structure for MySQL Database connection

```
"ConnectionStrings": {
    "DefaultConnection": "server=srvName;database=DBname;user=devhouse;password=userPassword"
  }
```

## Additional external libraries/packages used

- MySQL.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.AspNetCore.Authentication.JwtBearer
- System.IdentityModel.Tokens.Jwt
