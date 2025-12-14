# Portfolio

## About
This is my software developer portfolio website.<br>
It currently features 3 software applications that I have built with much care.<br>
I am continually updating the applications regularly, so check back frequently for new updates!<br>
Visit my site at [https://www.jmarcello.dev](https://www.jmarcello.dev)

## Featured Projects

# [The 4th Wall Cafe](https://www.jmarcello.dev/Home/Cafe)
This is a _full-stack_ web application that utilizes **ASP.NET Core (MVC)** for the _front-end_ portion.<br>
The _back-end_ is written in **C#** and utilizes both **Entity Framework Core** and **Dapper** (Micro-ORM) for data persistence.<br>
**ASP.NET Core Identity** is also used for authentication and authorization.

<img width="800" height="560" alt="4thwall" src="https://github.com/user-attachments/assets/72e0bfa8-8256-49b3-91f2-3b6cab8be045" />

## Application Features

### Two Database Modes
There are two _different_ database modes that can be set up for effective data persistence.<br>
**1.) Entity Framework Core:** A framework ideal for _light weight queries_.<br>
**2.) Dapper:** A micro-ORM ideal for _greater granular control_.<br>

>[!NOTE]
>In order to switch between database modes, simply change the value for the "DatabaseMode" key found in the **appsettings.json** file.<br>
>The default value; "ORM" is set up to use Entity Framework Core. If you would like to use Dapper, change the value to "Dapper".

##

# [Cafe API](https://www.jmarcello.dev/Home/CafeAPI)
This is a REST API that utilizes **ASP.NET Core (Web API)** for its controllers.<br>
It uses **The 4th Wall Cafe** _back-end_ for its business logic and data persistence.<br>
**ASP.NET Core Identity** is also used for authentication and authorization.

![API1](https://github.com/user-attachments/assets/d1118f85-8740-46d0-8d97-01b19f087604)

##

### [Cat Poker](https://www.jmarcello.dev/cat-poker/index.html)
A web application made with HTML, CSS, JavaScript, and Bootstrap.

![catpoker1](https://github.com/user-attachments/assets/423cff31-939d-4a9f-9427-72faf13b696d)


## More projects and features coming soon!

>[!IMPORTANT]
>The 4th Wall Cafe website can now process orders and simulate payment.
